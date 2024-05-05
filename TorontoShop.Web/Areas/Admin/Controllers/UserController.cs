using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;
using TorontoShop.Application.Services;
using TorontoShop.Domain.ViewModel.Admin.Account;

namespace TorontoShop.Web.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices=userServices;
        }
        public async Task<IActionResult> FilterUser(FilterUserViewModel filter)
        {
            var data = await _userServices.FilterUsers(filter);

            return View(data);
        }

    }
}
