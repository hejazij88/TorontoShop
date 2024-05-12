using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.ViewModel.Admin.Product;

namespace TorontoShop.Domain.Interfaces;

public interface IProductRepository
{

    Task SaveChangeAsync();
    Task<bool> CheckUrlNameCategory(string url);

    Task AddProductCategory(ProductCategory productCategory);

    Task<bool> CheckUrlNameCategories(string urlName, Guid CategoryId);
    Task<ProductCategory> GetProductCategoryById(Guid id);
    void UpdateProductCtaegory(ProductCategory category);
    Task<ProductCategoryFilterViewModel> FilterProductCategories(ProductCategoryFilterViewModel filter);

    Task<FilterProductViewModel> FilterProduct(FilterProductViewModel filterProductViewModel);

}