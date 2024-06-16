using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.ViewModel.Admin.Product;

namespace TorontoShop.Web.Controllers
{
    public class ProductController : SiteBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")]
        public async Task<IActionResult> Products(FilterProductViewModel filterProductViewModel)
        {
            filterProductViewModel.TakeEntity = 12;
            filterProductViewModel.ProductBox = ProductBox.ItemBoxInSite;
            return View(await _productService.FilterProduct(filterProductViewModel));
        }
    }
}
