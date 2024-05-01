using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.ViewModel.Accounts;

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
            if (user.IsMobileActive==false) return LogInUserStatus.NoActive;

            if (user.Password != _passwordHelper.EncodePasswordMd5(logInViewModel.Password))
                return LogInUserStatus.NotFound;


            return LogInUserStatus.Success;

        }

        public async Task<ActiveCodeResult> ActiveCodeAsync(ActiveCodeViewModel activeCodeViewModel)
        {
            var user =await _userRepository.GetUserByPhoneNumber(activeCodeViewModel.Phone);
            if (user == null) return ActiveCodeResult.NotFound;

            if (user.MobileActiveCode == activeCodeViewModel.ActiveCode)
            {
                user.MobileActiveCode = new Random().Next(10000, 99999).ToString();
                user.IsMobileActive=true;
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
    }
}
