using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.ViewModel.Admin.Product;

namespace TorontoShop.Web.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FilterProductCategories(ProductCategoryFilterViewModel filter)
        {
            return View(await _productService.ProductCategoryFilter(filter));
        }

    }
}
