using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.ViewModel.Accounts;

namespace TorontoShop.Web.Controllers
{
    public class AccountController : SiteBaseController
    {

        private readonly IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.RegisterUserAsync(registerViewModel);
                switch (result)
                {
                    case RegisterUserStatus.PhoneExist:
                        break;
                    case RegisterUserStatus.Success:
                        break;
                }
            }
            return View(registerViewModel);
        }



        [HttpGet("LogIn")]
        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost("LogIn"), ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.LogInUserAsync(logInViewModel);

                switch (result)
                {
                    case LogInUserStatus.IsBlocked:
                        break;
                    case LogInUserStatus.NotFound:
                        break;
                    case LogInUserStatus.NoActive:
                        break;
                    case LogInUserStatus.Success:
                        var user = await _userServices.GetUserByPhoneAsync(logInViewModel.PhoneNumber);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,logInViewModel.PhoneNumber),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                        };

                        var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principle = new ClaimsPrincipal(Identity);
                        var property = new AuthenticationProperties()
                        {
                            IsPersistent = logInViewModel.IsRememberMe
                        };
                        await HttpContext.SignInAsync(principle, property);
                        return Redirect("/");
                }
            }
            return View(logInViewModel);
        }
    }
}
