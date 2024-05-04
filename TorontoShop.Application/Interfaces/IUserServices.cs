using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        Task<User> GetUserById(Guid userId);

        Task<EditUserProfileViewModel> GetUserProfile(Guid id);

        Task<EditUserProfileResult> EditUserProfileAsync(Guid id, IFormFile formFile, EditUserProfileViewModel editUserProfileViewModel);



    }
}
