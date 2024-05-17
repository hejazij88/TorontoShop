using TorontoShop.Domain.Model.Site;
using TorontoShop.Domain.ViewModel.Site.Slider;

namespace TorontoShop.Domain.Interfaces;

public interface ISliderRepository
{
    Task<FilterSlidersViewModel> FilterSliders(FilterSlidersViewModel filterSlidersViewModel);
    Task<Slide> GetSlide(Guid slideId);
    Task<bool> DeleteSlide(Guid slideId);
    void UpdateSlide(Slide slide);
    Task AddSlide(Slide slide);
    Task SaveChange();

}