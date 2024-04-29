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
        public UserServices(IUserRepository userRepository, IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
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

                return RegisterUserStatus.Success;
            }

            return RegisterUserStatus.PhoneExist;
        }

        public async Task<LogInUserStatus> LogInUserAsync(LogInViewModel logInViewModel)
        {
            var user = await _userRepository.GetUserByPhoneNumber(logInViewModel.PhoneNumber);

            if (user == null) return LogInUserStatus.NotFound;
            if (user.IsBlocked) return LogInUserStatus.IsBlocked;
            if (user.IsMobileActive) return LogInUserStatus.NoActive;

            if (user.Password != _passwordHelper.EncodePasswordMd5(logInViewModel.Password))
                return LogInUserStatus.NotFound;


            return LogInUserStatus.Success;

        }

        public async Task<User?> GetUserByPhoneAsync(string phone)
        {
            return await _userRepository.GetUserByPhoneNumber(phone);
        }
    }
}
