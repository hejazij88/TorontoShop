using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TorontoShop.Application.Interfaces;

namespace TorontoShop.Web.ViewComponents
{
    #region site header
    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly IUserServices _userServices;
        public SiteHeaderViewComponent(IUserServices userServices)
        {
            _userServices= userServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
               ViewBag.User=await _userServices.GetUserByPhoneAsync(User.Identity.Name);
            }

            return View("SiteHeader");
        }
    }
    #endregion

    #region site footer
    public class SiteFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteFooter");
        }
    }
    #endregion
}
