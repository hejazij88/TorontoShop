using Microsoft.AspNetCore.Http;
using TorontoShop.Application.Extension;
using TorontoShop.Application.Interfaces;
using TorontoShop.Application.Utils;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.ViewModel.Accounts;
using TorontoShop.Domain.ViewModel.Admin.Account;

namespace TorontoShop.Application.Services
{
    public class UserServices : IUserServices
    {
        #region constractor
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly ISmsService _smsService;
        public UserServices(IUserRepository userRepository, IPasswordHelper passwordHelper, ISmsService smsService)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _smsService = smsService;
        }
        #endregion


        public async Task<RegisterUserStatus> RegisterUserAsync(RegisterViewModel registerViewModel)
        {
            if (!await _userRepository.IsUserPhoneExist(registerViewModel.PhoneNumber))
            {
                User user = new User
                {
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    Avatar = "Defult.png",
                    Gender = Gender.UnKnown,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    MobileActiveCode = new Random().Next(10000, 99999).ToString(),
                    Password = _passwordHelper.EncodePasswordMd5(registerViewModel.Password)
                };

                await _userRepository.RegisterUser(user);
                await _userRepository.SaveChange();
                await _smsService.SendVerificationCode(user.PhoneNumber, user.MobileActiveCode);


                return RegisterUserStatus.Success;
            }

            return RegisterUserStatus.PhoneExist;
        }

        public async Task<LogInUserStatus> LogInUserAsync(LogInViewModel logInViewModel)
        {
            var user = await _userRepository.GetUserByPhoneNumber(logInViewModel.PhoneNumber);

            if (user == null) return LogInUserStatus.NotFound;
            if (user.IsBlocked) return LogInUserStatus.IsBlocked;
            if (user.IsMobileActive == false) return LogInUserStatus.NoActive;

            if (user.Password != _passwordHelper.EncodePasswordMd5(logInViewModel.Password))
                return LogInUserStatus.NotFound;


            return LogInUserStatus.Success;

        }

        public async Task<ActiveCodeResult> ActiveCodeAsync(ActiveCodeViewModel activeCodeViewModel)
        {
            var user = await _userRepository.GetUserByPhoneNumber(activeCodeViewModel.Phone);
            if (user == null) return ActiveCodeResult.NotFound;

            if (user.MobileActiveCode == activeCodeViewModel.ActiveCode)
            {
                user.MobileActiveCode = new Random().Next(10000, 99999).ToString();
                user.IsMobileActive = true;
                _userRepository.UpdateUser(user);
                await _userRepository.SaveChange();
                return ActiveCodeResult.Success;
            }

            return ActiveCodeResult.Error;
        }

        public async Task<User?> GetUserByPhoneAsync(string phone)
        {
            return await _userRepository.GetUserByPhoneNumber(phone);
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<EditUserProfileViewModel> GetUserProfile(Guid id)
        {
            var user=await _userRepository.GetUserById(id);
            if (user == null) return null;

            EditUserProfileViewModel editUserProfileViewModel= new EditUserProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber
            };
            return editUserProfileViewModel;
        }

        public async Task<EditUserProfileResult> EditUserProfileAsync(Guid id, IFormFile avatar, EditUserProfileViewModel editUserProfileViewModel)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) return EditUserProfileResult.NotFound;

            user.FirstName = editUserProfileViewModel.FirstName;
            user.LastName = editUserProfileViewModel.LastName;
            user.Gender = editUserProfileViewModel.Gender;

            if (avatar != null && avatar.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N")+ Path.GetExtension(avatar.FileName);
                avatar.AddImageToServer(imageName, PathExtensions.UserAvatarOriginServer, 150, 150,
                    PathExtensions.UserAvatarThumbServer);
                user.Avatar = imageName;
            }

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChange();

            return EditUserProfileResult.Success;
        }

        public async Task<ChangePasswordResult> ChangePasswordAsync(Guid id, ChangePasswordViewModel changePasswordViewModel)
        {
            var user=await _userRepository.GetUserById(id);
            if (user != null)
            {
                var newPassword = _passwordHelper.EncodePasswordMd5(changePasswordViewModel.NewPassword);
                if (user.Password != newPassword)
                {
                    user.Password= newPassword;
                    _userRepository.UpdateUser(user);
                    await _userRepository.SaveChange();
                    return ChangePasswordResult.Success;
                }
                return ChangePasswordResult.PasswordEqual;
            }

            return ChangePasswordResult.NotFound;

        }

        public async Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filterUserViewModel)
        {
            return await _userRepository.FilterUsers(filterUserViewModel);
        }

        public async Task<EditUserFromAdmin> GetEditUserFromAdmin(Guid userId)
        {
            return await _userRepository.GetEditUserFromAdmin(userId);
        }

        public async Task<EditUserFromAdminResult> EditUserFromAdmin(EditUserFromAdmin editUser)
        {
            var user = await _userRepository.GetUserById(editUser.UserId);

            if (user == null)
                return EditUserFromAdminResult.NotFound;


            user.FirstName = editUser.FirstName;
            user.LastName = editUser.LastName;
            user.Gender = editUser.UserGender;

            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = _passwordHelper.EncodePasswordMd5(editUser.Password);
            }

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChange();

            return EditUserFromAdminResult.Success;

        }

        public async Task<CreateOrEditRoleViewModel> GetEditRoleById(Guid roleId)
        {
            return await _userRepository.GetEditRoleById(roleId);
        }

        public async Task<CreateOrEditRoleResult> CreateOrEditRole(CreateOrEditRoleViewModel createOrEditRole)
        {
            if (createOrEditRole.Id != Guid.Empty)
            {
                var role = await _userRepository.GetRoleById(createOrEditRole.Id);

                if (role == null)
                    return CreateOrEditRoleResult.NotFound;

                role.RoleTitle = createOrEditRole.RoleTitle;

                _userRepository.UpdateRole(role);

                await _userRepository.RemoveAllPermissionSelectedRole(createOrEditRole.Id);

                if (createOrEditRole.SelectedPermission == null)
                {
                    return CreateOrEditRoleResult.NotExistPermissions;
                }
                await _userRepository.AddPermissionToRole(createOrEditRole.SelectedPermission, createOrEditRole.Id);
                await _userRepository.SaveChange();

                return CreateOrEditRoleResult.Success;
            }
            else
            {
                //create

                var newRole = new Role
                {
                    RoleTitle = createOrEditRole.RoleTitle
                };

                await _userRepository.CreateRole(newRole);

                if (createOrEditRole.SelectedPermission == null)
                {
                    return CreateOrEditRoleResult.NotExistPermissions;
                }

                await _userRepository.AddPermissionToRole(createOrEditRole.SelectedPermission, newRole.Id);

                await _userRepository.SaveChange();


                return CreateOrEditRoleResult.Success;
            }
        }

        public async Task<FilterRolesViewModel> FilterRoles(FilterRolesViewModel filter)
        {
            return await _userRepository.FilterRoles(filter);
        }

        public async Task<List<Permission>> GetAllActivePermission()
        {
            return await _userRepository.GetAllActivePermission();

        }

        public async Task<List<Role>> GetAllActiveRoles()
        {
            return await _userRepository.GetAllActiveRoles();
        }

        public bool CheckPermission(Guid permissionId, string phoneNumber)
        {
            return _userRepository.CheckPermission(permissionId, phoneNumber);
        }
    }
}
