using Microsoft.AspNetCore.Mvc;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.ViewModel.Site.Slider;

namespace TorontoShop.Web.Areas.Admin.Controllers
{
    public class SliderController : AdminBaseController
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> FilterSlider(FilterSlidersViewModel filter)
        {
            return View(await _sliderService.FilterSliders(filter));
        }


        [HttpGet]
        public IActionResult CreateSlider()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSlider(CreateSliderViewModel sliderViewModel,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var result = await _sliderService.AddSlider(sliderViewModel,image);
                switch (result)
                {
                    case CreateSliderResult.Success:
                        TempData[SuccessMessage] = "با موفقیت ثبت شد";
                       return RedirectToAction("FilterSlider");
                }
            }

            return View(sliderViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> EditSlider(Guid sliderId)
        {
            return View(await _sliderService.GetSlider(sliderId));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSlider(EditSliderViewModel editSliderViewModel, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                var result = await _sliderService.EditSlider(editSliderViewModel, image);
                switch (result)
                {
                    case EditSliderResult.NotFound:
                        TempData[ErrorMessage] = "چنین اسلایدی وجود ندارد";
                        break;
                    case EditSliderResult.Success:
                        TempData[SuccessMessage] = "با موفقیت ثبت شد";
                        return RedirectToAction("FilterSlider");
                }
            }
            return View(editSliderViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSlider(Guid sliderId)
        {
            var result = await _sliderService.DeleteSlider(sliderId);
            if (result != null)
            {
                TempData[SuccessMessage] = "با موفقیت حذف شد";
                return RedirectToAction("FilterSlider");
            }

            TempData[ErrorMessage] = "مشکلی در حذف کردن وجود دارد";
            return RedirectToAction("FilterSlider");
        }


    }
}
