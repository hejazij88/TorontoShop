using Microsoft.AspNetCore.Mvc;

namespace TorontoShop.Web.Areas.PanelUser.Controllers
{
    public class HomeController : UserBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
