using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;
using TorontoShop.Application.Services;
using TorontoShop.Domain.ViewModel.Admin.Account;

namespace TorontoShop.Web.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices=userServices;
        }
        public async Task<IActionResult> FilterUser(FilterUserViewModel filter)
        {
            var data = await _userServices.FilterUsers(filter);

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(Guid userId)
        {
            var data = await _userServices.GetEditUserFromAdmin(userId);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserFromAdmin editUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.EditUserFromAdmin(editUser);

                switch (result)
                {
                    case EditUserFromAdminResult.NotFound:
                        TempData[WarningMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                        break;
                    case EditUserFromAdminResult.Success:
                        TempData[SuccessMessage] = "ویراش کاربر با موفقیت انجام شد";
                        return RedirectToAction("FilterUser");
                }
            }

            return View(editUser);
        }

    }
}
