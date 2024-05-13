using System.ComponentModel.DataAnnotations;

namespace TorontoShop.Domain.ViewModel.Admin.Product;

public class EditProductViewModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(500), MinLength(5)]
    [Display(Name = "نام محصول")]
    public string Name { get; set; }

    [Required]
    [MaxLength(500), MinLength(5)]
    [Display(Name = "توضیحات کوتاه")]
    public string ShortDescription { get; set; }

    [Required]
    [MaxLength(500), MinLength(5)]
    [Display(Name = "توضیحات")]
    public string Description { get; set; }

    [Required]
    [Display(Name = "قیمت")]
    public int Price { get; set; }

    [Display(Name = "فعال / غیرفعال")]
    public bool IsActive { get; set; }

    public string ImageName { get; set; }

    public List<Guid> ProductSelectedCategory { get; set; }
}

public enum EditProductResult
{
    Success,
    NotFound,
    CategoryIsNull
}