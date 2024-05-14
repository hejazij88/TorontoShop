using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TorontoShop.Application.Interfaces;
using TorontoShop.Application.Services;

namespace TorontoShop.Web.Permission;

public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private IUserServices _userServices;
    private Guid _permissionId = Guid.Empty;
    public PermissionCheckerAttribute(Guid permissionId)
    {
        _permissionId = permissionId;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _userServices = (IUserServices)context.HttpContext.RequestServices.GetService(typeof(IUserServices));

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            var phoneNumber = context.HttpContext.User.Identity.Name;

            if (!_userServices.CheckPermission(_permissionId, phoneNumber))
            {
                context.Result = new RedirectResult("/LogIn");
            }
        }
        else
        {
            context.Result = new RedirectResult("/LogIn");
        }
    }
}