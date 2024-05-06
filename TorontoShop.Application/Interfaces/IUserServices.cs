using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.ViewModel.Accounts;
using TorontoShop.Domain.ViewModel.Admin.Account;

namespace TorontoShop.Application.Interfaces
{
    public interface IUserServices
    {

        Task<RegisterUserStatus> RegisterUserAsync(RegisterViewModel registerViewModel);
        Task<LogInUserStatus> LogInUserAsync(LogInViewModel logInViewModel);

        Task<ActiveCodeResult> ActiveCodeAsync(ActiveCodeViewModel activeCodeViewModel);
        Task<User> GetUserByPhoneAsync(string phone);
        Task<User> GetUserById(Guid userId);

        Task<EditUserProfileViewModel> GetUserProfile(Guid id);

        Task<EditUserProfileResult> EditUserProfileAsync(Guid id, IFormFile formFile, EditUserProfileViewModel editUserProfileViewModel);
        
        Task<ChangePasswordResult> ChangePasswordAsync(Guid id, ChangePasswordViewModel changePasswordViewModel);

        Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filterUserViewModel);

        Task<EditUserFromAdmin> GetEditUserFromAdmin(Guid userId);
        Task<EditUserFromAdminResult> EditUserFromAdmin(EditUserFromAdmin editUser);


        Task<CreateOrEditRoleViewModel> GetEditRoleById(Guid roleId);
        Task<CreateOrEditRoleResult> CreateOrEditRole(CreateOrEditRoleViewModel createOrEditRole);
        Task<FilterRolesViewModel> FilterRoles(FilterRolesViewModel filter);
        Task<List<Permission>> GetAllActivePermission();
        Task<List<Role>> GetAllActiveRoles();








    }
}
