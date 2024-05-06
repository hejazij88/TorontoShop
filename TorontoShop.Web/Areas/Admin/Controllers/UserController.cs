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
            ViewData["Roles"] = await _userServices.GetAllActiveRoles();

            return View(data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserFromAdmin editUser)
        {
            ViewData["Roles"] = await _userServices.GetAllActiveRoles();

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


        public async Task<IActionResult> FilterRoles(FilterRolesViewModel filter)
        {
            return View(await _userServices.FilterRoles(filter));
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateOrEditRoleViewModel create)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.CreateOrEditRole(create);

                switch (result)
                {
                    case CreateOrEditRoleResult.NotFound:
                        break;
                    case CreateOrEditRoleResult.NotExistPermissions:
                        TempData[WarningMessage] = "لطفا نقشی را انتخاب کنید";
                        break;
                    case CreateOrEditRoleResult.Success:
                        TempData[SuccessMessage] = "عملیات افزودن نقش با موفقیت انجام شد";
                        return RedirectToAction("FilterRoles");
                }
            }

            return View(create);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(Guid roleId)
        {
            ViewData["Permissions"] = await _userServices.GetAllActivePermission();
            return View(await _userServices.GetEditRoleById(roleId));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(CreateOrEditRoleViewModel create)
        {
            ViewData["Permissions"] = await _userServices.GetAllActivePermission();
            if (ModelState.IsValid)
            {
                var result = await _userServices.CreateOrEditRole(create);

                switch (result)
                {
                    case CreateOrEditRoleResult.NotFound:
                        TempData[WarningMessage] = "نقشی با مشخصات وارد شده یافت نشد";
                        break;
                    case CreateOrEditRoleResult.NotExistPermissions:
                        TempData[WarningMessage] = "لطفا نقشی را انتخاب کنید";
                        break;
                    case CreateOrEditRoleResult.Success:
                        TempData[SuccessMessage] = "عملیات ویرایش نقش با موفقیت انجام شد";
                        return RedirectToAction("FilterRoles");
                }
            }

            return View(create);
        }

    }
}
