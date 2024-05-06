using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.ViewModel.Admin.Account;
using TorontoShop.Domain.ViewModel.Paging;

namespace TorontoShop.Domain.ViewModel.Admin.Product
{
    public class ProductCategoryFilterViewModel:BasePaging
    {
        public string Title { get; set; }
        public List<ProductCategory> Categories { get; set; }

        public ProductCategoryFilterViewModel SetCategory(List<ProductCategory> categories)
        {
            this.Categories = categories;
            return this;
        }

        public ProductCategoryFilterViewModel SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntityCount = paging.AllEntityCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.CountForShowAfterAndBefor = paging.CountForShowAfterAndBefor;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;

            return this;
        }


    }
}
