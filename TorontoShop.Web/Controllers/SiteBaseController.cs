using Microsoft.AspNetCore.Mvc;

namespace TorontoShop.Web.Controllers
{
    public class SiteBaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
