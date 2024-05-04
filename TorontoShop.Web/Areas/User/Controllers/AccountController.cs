using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.ViewModel.Accounts;
using TorontoShop.Web.Areas.PanelUser.Controllers;
using TorontoShop.Web.Extensions;

namespace TorontoShop.Web.Areas.User.Controllers
{
    public class AccountController : UserBaseController
    {
        private readonly IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices= userServices;
        }


        [HttpGet("edit-user-profile")]
        public async Task<IActionResult> EditUserProfile()
        {
            var user =await _userServices.GetUserProfile(User.GetUserId());
            if (user == null) return NotFound();

            return View(user);
        }


        [HttpPost("edit-user-profile"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editUserProfileViewModel, IFormFile avatar)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.EditUserProfileAsync(User.GetUserId(),avatar,editUserProfileViewModel);
                switch (result)
                {
                    case EditUserProfileResult.NotFound:
                        TempData[ErrorMessage] = "کاربری پیدا نشد";
                        break;
                    case EditUserProfileResult.Success:
                        TempData[SuccessMessage] = "با موفقیت ذخیره شد";
                       return RedirectToAction("EditUserProfile");
                }
            }

            return View(editUserProfileViewModel);
        }

        [HttpGet("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost("change-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.ChangePasswordAsync(User.GetUserId(), changePasswordViewModel);
                switch (result)
                {
                    case ChangePasswordResult.NotFound:
                        TempData[ErrorMessage] = "کاربری پیدا نشد";
                        break;
                    case ChangePasswordResult.PasswordEqual:
                        TempData[ErrorMessage] = "رمز جدید با رمز فعلی متفاوت نیست";
                        break;
                    case ChangePasswordResult.Success:
                        TempData[SuccessMessage] = "با موفقیت تغییر کرد";
                        TempData[InfoMessage] = "جهت تکمیل فرایند مجدد وارد شوید";

                        await HttpContext.SignOutAsync();
                        return RedirectToAction("LogIn", "Account", new { area = "" });
                }
            }
            return View(changePasswordViewModel);
        }
    }
}
