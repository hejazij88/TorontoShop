using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TorontoShop.Application.Interfaces;
using TorontoShop.Web.Models;

namespace TorontoShop.Web.Controllers
{
    public class HomeController : SiteBaseController
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService=productService;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["LastProduct"] = await _productService.LastProducts();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
