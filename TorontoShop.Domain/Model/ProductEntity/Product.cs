using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.BaseEntities;

namespace TorontoShop.Domain.Model.ProductEntity
{
    [Table("Product")]
    public class Product : BaseEntity
    {

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

        [Required]
        [MaxLength(500), MinLength(5)]
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }

        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }


        [InverseProperty(nameof(ProductFuture.Product))] public ICollection<ProductFuture> Futures { get; set; }
        [InverseProperty(nameof(ProductEntity.ProductSelectedCategory.Product))] public ICollection<ProductSelectedCategory> ProductSelectedCategory { get; set; }
        [InverseProperty(nameof(ProductGallery.Product))] public ICollection<ProductGallery> Galleries { get; set; }
    }
}
