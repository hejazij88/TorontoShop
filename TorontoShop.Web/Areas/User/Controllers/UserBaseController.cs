using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TorontoShop.Web.Areas.PanelUser.Controllers
{
    [Authorize]
    [Area("User")]
    [Route("user")]
    public class UserBaseController : Controller
    {
            protected string ErrorMessage = "ErrorMessage";
            protected string SuccessMessage = "SuccessMessage";
            protected string WarningMessage = "WarningMessage";
            protected string InfoMessage = "InfoMessage";
    }
}
