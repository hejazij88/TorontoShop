using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.BaseEntities;

namespace TorontoShop.Domain.Model.ProductEntity
{
    public class ProductSelectedCategory:BaseEntity
    {
        public Guid ProductId { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(ProductId))] public virtual Product Product { get; set; }

        [ForeignKey(nameof(CategoryId))]public virtual ProductCategory ProductCategory { get; set; }

    }
}
