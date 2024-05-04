using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.ViewModel.Accounts;
using TorontoShop.Domain.ViewModel.Wallet;

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
                        TempData[ErrorMessage] = "شماره موبایل در پایگاه داده وجود دارد";
                        break;
                    case RegisterUserStatus.Success:
                        TempData[SuccessMessage] = "ثبت نام موفقیت آمیز بود";
                        return RedirectToAction("ActivateCode", "Account", new { mobile = registerViewModel.PhoneNumber });

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
                        TempData[ErrorMessage] = "حساب شما مسدود شده است";
                        break;
                    case LogInUserStatus.NotFound:
                        TempData[ErrorMessage] = "حساب کاربری پیدا نشد";
                        break;
                    case LogInUserStatus.NoActive:
                        TempData[ErrorMessage] = "حساب شما فعال نشده است";
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
                        TempData[SuccessMessage] = "ورود موفقیت آمیز";
                        return Redirect("/");
                }
            }
            return View(logInViewModel);
        }


        [HttpGet("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            TempData[InfoMessage] = "خروج موفقیت آمیز";
          return  Redirect("/");
        }

        [HttpGet("Active-Code/{phone}")]
        public IActionResult ActivateCode(string phone)
        {
            if (User.Identity.IsAuthenticated)
                Redirect("/");

            var activecodeVM = new ActiveCodeViewModel { Phone = phone };

            return View(activecodeVM);
        }



        [HttpPost("Active-Code/{phone}"),ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateCode(ActiveCodeViewModel activeCodeViewModel)
        {
            if (ModelState.IsValid)
            {
                var result =await _userServices.ActiveCodeAsync(activeCodeViewModel);
                switch (result)
                {
                    case ActiveCodeResult.Error:
                        TempData[ErrorMessage] = "فعال سازی با مشکل روبه رو شد";
                    break;
                    case ActiveCodeResult.NotFound:
                        TempData[ErrorMessage] = "کاربری با چنین مشخصاتی پیدا نشد";
                        break;
                    case ActiveCodeResult.Success:
                        TempData[SuccessMessage] = "فعال سازی موفقیت آمیز بود";
                       return Redirect("LogIn");
                        
                }
            }

            return View(activeCodeViewModel);
        }

    }
}
