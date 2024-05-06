using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.BaseEntities;

namespace TorontoShop.Domain.Model.ProductEntity
{
    public class ProductGallery:BaseEntity
    {

        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(500), MinLength(5)]
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }

        public Product Product { get; set; }
    }
}
