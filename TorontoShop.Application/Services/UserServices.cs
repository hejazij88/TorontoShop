using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.Interfaces;

namespace TorontoShop.Application.Services
{
    public class UserServices: IUserServices
    {
        #region constractor
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion


    }
}
