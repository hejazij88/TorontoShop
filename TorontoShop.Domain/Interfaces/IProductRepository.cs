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
    Task AddProduct(Product product);
    Task RemoveProductSelectCategory(Guid productId);
    Task AddProductSelectCategory(List<Guid> productSelectCategory,Guid productId);
    Task<List<ProductCategory>> GetAllCategory(); 
    Task<Product> GetProductById(Guid productId);
    void UpdateProduct(Product product);
    Task<List<Guid>> GetAllProductCategoriesId(Guid productId);
    Task<bool> DeleteProduct(Guid productId);
    Task<bool> RecoveryProduct(Guid productId);




}