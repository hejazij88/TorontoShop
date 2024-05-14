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
    [Table("ProductGallery")]
    public class ProductGallery:BaseEntity
    {

        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(500), MinLength(5)]
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }

        [ForeignKey(nameof(ProductId))]public Product Product { get; set; }
    }
}
