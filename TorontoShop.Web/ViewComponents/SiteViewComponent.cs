using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.ViewModel.Admin.Product;
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


    #region site Slider - Home
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

            var data =await _sliderService.FilterSliders(filterSliderVM);
            return View("SliderHome",data);
        }
    }
    #endregion


    #region PopularCategory
    public class PopularCategoryViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public PopularCategoryViewComponent(IProductService productService)
        {
            _productService=productService ;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var filterCategoryVM = new ProductCategoryFilterViewModel()
            {
                TakeEntity = 6
            };

            var data = await _productService.ProductCategoryFilter(filterCategoryVM);
            return View("PopularCategory", data);
        }
    }
    #endregion
    #region SideBarCategory
    public class SideBarCategoryViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public SideBarCategoryViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var filterCategoryVM = new ProductCategoryFilterViewModel()
            {
                TakeEntity = 6
            };

            var data = await _productService.ProductCategoryFilter(filterCategoryVM);
            return View("SideBarCategory", data);
        }
    }
    #endregion
}
