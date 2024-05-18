using Microsoft.AspNetCore.Http;
using TorontoShop.Domain.ViewModel.Site.Slider;

namespace TorontoShop.Application.Interfaces;

public interface ISliderService
{
    Task<FilterSlidersViewModel>FilterSliders(FilterSlidersViewModel filterSlidersViewModel);
    Task<EditSliderViewModel> GetSlider(Guid sliderId);
    Task<CreateSliderResult> AddSlider(CreateSliderViewModel createSliderViewModel,IFormFile? image);
    Task<EditSliderResult> EditSlider(EditSliderViewModel editSliderViewModel,IFormFile? image);
    Task<bool> DeleteSlider(Guid SliderId);
}