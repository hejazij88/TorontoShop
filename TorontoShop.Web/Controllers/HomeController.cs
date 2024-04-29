using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TorontoShop.Web.Models;

namespace TorontoShop.Web.Controllers
{
    public class HomeController : SiteBaseController
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
