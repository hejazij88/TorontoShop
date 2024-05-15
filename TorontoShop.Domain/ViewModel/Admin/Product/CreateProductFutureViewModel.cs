using System.ComponentModel.DataAnnotations;

namespace TorontoShop.Domain.ViewModel.Admin.Product;

public class CreateProductFutureViewModel
{
    public Guid ProductId { get; set; }
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Title { get; set; }

    [Display(Name = "مقدار")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Value { get; set; }
}

public enum CreateProductFutureResult
{
    Success
    ,Error
}