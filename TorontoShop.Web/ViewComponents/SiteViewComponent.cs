using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.ViewModel.Site.Slider;

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


    #region site Sloder - Home
    public class SliderHomeViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;
        public SliderHomeViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var filterSliderVM = new FilterSlidersViewModel()
            {
                TakeEntity = 10
            };

            var data = _sliderService.FilterSliders(filterSliderVM);
            return View("SliderHome",data);
        }
    }
    #endregion
}
