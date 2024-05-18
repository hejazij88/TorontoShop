using Microsoft.AspNetCore.Http;
using TorontoShop.Application.Extension;
using TorontoShop.Application.Interfaces;
using TorontoShop.Application.Utils;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.Model.Site;
using TorontoShop.Domain.ViewModel.Admin.Product;
using TorontoShop.Domain.ViewModel.Site.Slider;

namespace TorontoShop.Application.Services;

public class SliderService : ISliderService
{
    private ISliderRepository _sliderRepository;

    public SliderService(ISliderRepository sliderRepository)
    {
        _sliderRepository = sliderRepository;
    }
    public async Task<FilterSlidersViewModel> FilterSliders(FilterSlidersViewModel filterSlidersViewModel)
    {
        return await _sliderRepository.FilterSliders(filterSlidersViewModel);
    }

    public async Task<EditSliderViewModel> GetSlider(Guid sliderId)
    {
        var data = await _sliderRepository.GetSlide(sliderId);
        if (data != null)
        {
            var sliderVM = new EditSliderViewModel
            {
                Id = data.Id,
                ButtonText = data.ButtonText,
                Href = data.Href,
                ImageName = data.ImageName,
                Price = data.Price,
                Text = data.Text,
                Title = data.Title
            };
            return sliderVM;
        }

        return null;
    }

    public async Task<CreateSliderResult> AddSlider(CreateSliderViewModel createSliderViewModel, IFormFile? image)
    {
        var Slider = new Slide()
        {
            Price = createSliderViewModel.Price,
            Title = createSliderViewModel.Title,
            ButtonText = createSliderViewModel.ButtonText,
            Href = createSliderViewModel.Href,
            Text = createSliderViewModel.Text
        };
        if (image != null && image.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
            image.AddImageToServer(imageName, PathExtensions.SliderOriginServer, 150, 150, PathExtensions.SliderThumbServer);

            Slider.ImageName = imageName;
        }

        await _sliderRepository.AddSlide(Slider);
        await _sliderRepository.SaveChange();

        return CreateSliderResult.Success;

    }

    public async Task<EditSliderResult> EditSlider(EditSliderViewModel editSliderViewModel, IFormFile? image)
    {
        var slider = await _sliderRepository.GetSlide(editSliderViewModel.Id);

        if (slider == null) return EditSliderResult.NotFound;


        slider.Price = editSliderViewModel.Price;
        slider.Title = editSliderViewModel.Title;
        slider.ButtonText = editSliderViewModel.ButtonText;
        slider.Href = editSliderViewModel.Href;
        slider.Text = editSliderViewModel.Text;

        if (image != null && image.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
            image.AddImageToServer(imageName, PathExtensions.SliderOriginServer, 150, 150, PathExtensions.SliderThumbServer, slider.ImageName);

            slider.ImageName = imageName;
        }

        _sliderRepository.UpdateSlide(slider);

        await _sliderRepository.SaveChange();

        return EditSliderResult.Success;
    }
}