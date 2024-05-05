using Microsoft.AspNetCore.Mvc;

namespace TorontoShop.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
