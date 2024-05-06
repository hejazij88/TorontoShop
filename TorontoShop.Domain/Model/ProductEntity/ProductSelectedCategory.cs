using System;
using System.Collections.Generic;
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

        public Product Product { get; set; }

        public ProductCategory ProductCategory { get; set; }

    }
}
