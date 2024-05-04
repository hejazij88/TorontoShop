using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.ViewModel.Accounts;
using TorontoShop.Domain.ViewModel.Wallet;
using TorontoShop.Web.Areas.PanelUser.Controllers;
using TorontoShop.Web.Extensions;
using ZarinpalSandbox;

namespace TorontoShop.Web.Areas.User.Controllers
{
    public class AccountController : UserBaseController
    {
        private readonly IUserServices _userServices;
        private readonly IUserWalletService _userWalletService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserServices userServices, IUserWalletService userWalletService, IConfiguration configuration)
        {
            _userServices = userServices;
            _userWalletService = userWalletService;
            _configuration = configuration;
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

        [HttpGet("charge-wallet")]
        public async Task<IActionResult> ChargeWallet()
        {
            return View();
        }


        [HttpPost("charge-wallet"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChargeWallet(ChargeUserWalletViewModel chargeUserWalletViewModel)
        {
            if (ModelState.IsValid)
            {
                var walletId = await _userWalletService.ChargeWalletAsync(User.GetUserId(), chargeUserWalletViewModel, $"شارژ به مبلغ {chargeUserWalletViewModel.Amount}");

                #region payment
                var payment = new Payment(chargeUserWalletViewModel.Amount);
                var url = _configuration.GetSection("DefaultUrl")["Host"] + "/user/online-payment/" + walletId;
                var result = payment.PaymentRequest("شارژ کیف پول", url);
                
                if (result.Result.Status == 100)
                {
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
                }
                else
                {
                    TempData[ErrorMessage] = "مشکلی در پرداخت به وجود آماده است،لطفا مجددا امتحان کنید";
                }

                #endregion
            }
            return View(chargeUserWalletViewModel);
        }
    }
}
