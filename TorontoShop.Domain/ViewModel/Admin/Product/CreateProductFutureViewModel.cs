namespace TorontoShop.Domain.ViewModel.Admin.Product;

public class CreateProductFutureViewModel
{
    public Guid ProductId { get; set; }
    public string Title { get; set; }
    public string Value { get; set; }
}

public enum CreateProductFutureResult
{
    Success
    ,Error
}