using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.ProductEntity;

namespace TorontoShop.Domain.ViewModel.Site.Products
{
    public class ProductItemViewModel
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }
        public string ProductImageName { get; set; }
        public int Price { get; set; }
        public ProductCategory ProductCategory  { get; set; }
        public int Count { get; set; }
        public string CategoryHref { get; set; }

    }
}
