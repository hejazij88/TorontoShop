using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;

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
    }
}
