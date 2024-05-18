namespace TorontoShop.Domain.ViewModel.Site.Slider;

public class EditSliderViewModel:CreateSliderViewModel
{
    public Guid Id { get; set; }
    public string? ImageName { get; set; }
}

public enum EditSliderResult
{
    Success,
    NotFound
}