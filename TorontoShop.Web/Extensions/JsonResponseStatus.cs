using Microsoft.AspNetCore.Mvc;

namespace TorontoShop.Web.Extensions
{
    public class JsonResponseStatus
    {
        public static JsonResult Success()
        {
            return new JsonResult(new { status = "Success" });
        }
        public static JsonResult Error()
        {
            return new JsonResult(new { status = "Error" });
        }
    }
}
