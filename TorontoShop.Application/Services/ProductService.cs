using Microsoft.AspNetCore.Http;
using TorontoShop.Application.Extension;
using TorontoShop.Application.Interfaces;
using TorontoShop.Application.Utils;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.ViewModel.Admin.Product;

namespace TorontoShop.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<CreateProductCategoryResult> CreateProductCategory(CreateProductCategoryViewModel categoryViewModel, IFormFile file)
    {
        if (await _productRepository.CheckUrlNameCategory(categoryViewModel.UrlName)) return CreateProductCategoryResult.IsExist;

        var newCategory = new ProductCategory()
        {
            Title = categoryViewModel.Title,
            UrlName = categoryViewModel.UrlName
        };
        if (file != null && file.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            file.AddImageToServer(imageName, PathExtensions.CategoryOrginServer, 150, 150, PathExtensions.CategoryThumbServer);

            newCategory.ImageName = imageName;
        }

        await _productRepository.AddProductCategory(newCategory);
        await _productRepository.SaveChangeAsync();

        return CreateProductCategoryResult.Success;

    }

    public async Task<EditProductCategoryResult> EditProductCategory(EditProductCategoryViewModel categoryViewModel, IFormFile file)
    {
        var productCategory = await _productRepository.GetProductCategoryById(categoryViewModel.CategoryId);

        if (productCategory == null) return EditProductCategoryResult.NotFound;

        if (await _productRepository.CheckUrlNameCategories(categoryViewModel.UrlName, categoryViewModel.CategoryId)) return EditProductCategoryResult.IsExist;

        productCategory.UrlName = categoryViewModel.UrlName;
        productCategory.Title = categoryViewModel.Title;

        if (file != null && file.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            file.AddImageToServer(imageName, PathExtensions.CategoryOrginServer, 150, 150, PathExtensions.CategoryThumbServer, productCategory.ImageName);

            productCategory.ImageName = imageName;
        }

        _productRepository.UpdateProductCtaegory(productCategory);

        await _productRepository.SaveChangeAsync();

        return EditProductCategoryResult.Success;
    }

    public async Task<EditProductCategoryViewModel> GetEditProductCategory(Guid productCategoryId)
    {
        var productcategory = await _productRepository.GetProductCategoryById(productCategoryId);

        if (productcategory != null)
        {
            return new EditProductCategoryViewModel
            {
                ImageName = productcategory.ImageName,
                CategoryId = productcategory.Id,
                Title = productcategory.Title,
                UrlName = productcategory.UrlName
            };
        }

        return null;
    }

    public async Task<ProductCategoryFilterViewModel> ProductCategoryFilter(ProductCategoryFilterViewModel categoryFilterViewModel)
    {
        return await _productRepository.FilterProductCategories(categoryFilterViewModel);
    }
}