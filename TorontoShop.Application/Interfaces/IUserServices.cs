using System.Collections.Generic;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.ViewModel.Accounts;

namespace TorontoShop.Application.Interfaces
{
    public interface IUserServices
    {

        Task<RegisterUserStatus> RegisterUserAsync(RegisterViewModel registerViewModel);
        Task<LogInUserStatus> LogInUserAsync(LogInViewModel logInViewModel);

        Task<ActiveCodeResult> ActiveCodeAsync(ActiveCodeViewModel activeCodeViewModel);
        Task<User> GetUserByPhoneAsync(string phone);


    }
}
