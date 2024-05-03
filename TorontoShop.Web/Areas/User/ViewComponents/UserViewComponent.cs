using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using TorontoShop.Application.Interfaces;
using TorontoShop.Web.Extensions;

namespace TorontoShop.Web.Areas.PanelUser.ViewComponents
{
    public class UserSideBarViewComponent:ViewComponent
    {
        #region constrator
        private readonly IUserServices _userService;
        public UserSideBarViewComponent(IUserServices userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserById(User.GetUserId());
                return View("UserSideBar", user);
            }

            return View("UserSideBar");
        }
        #endregion
    }

    public class UserInformationViewComponent : ViewComponent
    {
        #region constrator
        private readonly IUserServices _userService;
        public UserInformationViewComponent(IUserServices userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserById(User.GetUserId());
                return View("UserInformation", user);
            }

            return View("UserInformation");
        }
        #endregion
    }
}
