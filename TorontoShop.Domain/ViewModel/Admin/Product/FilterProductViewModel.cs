using System.ComponentModel.DataAnnotations;
using TorontoShop.Domain.ViewModel.Paging;
using TorontoShop.Domain.ViewModel.Site.Products;

namespace TorontoShop.Domain.ViewModel.Admin.Product
{
    public class FilterProductViewModel:BasePaging
    {
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public List<Model.ProductEntity.Product> Products { get; set; }
        public List<ProductItemViewModel> ProductItem { get; set; }

        public ProductState State { get; set; }
        public ProductOrder ProductOrder { get; set; }
        public ProductBox ProductBox { get; set; }



        public FilterProductViewModel SetProduct(List<Model.ProductEntity.Product> products)
        {
            this.Products = products;
            return this;
        }

        public FilterProductViewModel SetProductItem(List<ProductItemViewModel> itemViewModels)
        {
            ProductItem=itemViewModels;
            return this;
        }

        public FilterProductViewModel SetPaging(BasePaging paging)
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

    public enum ProductState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فعال")]
        IsActive,
        [Display(Name = "حذف شده")]
        Delete
    }

    public enum ProductOrder
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "جدید ترین")]
        News,
        [Display(Name = "گران ترین")]
        Expensive,
        [Display(Name = "ارزان ترین")]
        Chip,
    }

    public enum ProductBox
    {
        Default,
        ItemBoxInSite,


    }
}
