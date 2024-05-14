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
            file.AddImageToServer(imageName, PathExtensions.CategoryOriginServer, 150, 150, PathExtensions.CategoryThumbServer);

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
            file.AddImageToServer(imageName, PathExtensions.CategoryOriginServer, 150, 150, PathExtensions.CategoryThumbServer, productCategory.ImageName);

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

    public async Task<FilterProductViewModel> FilterProduct(FilterProductViewModel filterProductViewModel)
    {
        return await _productRepository.FilterProduct(filterProductViewModel);
    }

    public async Task<CreateProductResult> CreateProduct(CreateProductViewModel createProductViewModel, IFormFile image)
    {

        var product = new Product
        {
            Description = createProductViewModel.Description,
            ShortDescription = createProductViewModel.ShortDescription,
            IsActive = createProductViewModel.IsActive,
            Name = createProductViewModel.Name,
            Price = createProductViewModel.Price
        };

        if (image != null && image.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);

            image.AddImageToServer(imageName, PathExtensions.CategoryOriginServer, 257, 273,
                PathExtensions.ProductThumbServer);

            product.ImageName = imageName;
        }
        else
        {
            return CreateProductResult.NotHaveImage;
        }

        await _productRepository.AddProduct(product);
        await _productRepository.SaveChangeAsync();

        await _productRepository.AddProductSelectCategory(createProductViewModel.ProductSelectedCategory, product.Id);


        return CreateProductResult.Success;
    }

    public async Task<List<ProductCategory>> GetAllCategory()
    {
        return await _productRepository.GetAllCategory();
    }

    public async Task<EditProductViewModel> GetEditProduct(Guid productId)
    {
        var result = await _productRepository.GetProductById(productId);

        return  new EditProductViewModel
        {
            Id = productId,
            Description = result.Description,
            IsActive = result.IsActive,
            Name = result.Name,
            Price = result.Price,
            ShortDescription = result.ShortDescription,
            ImageName = result.ImageName,
            ProductSelectedCategory = await _productRepository.GetAllProductCategoriesId(productId)

        };
    }

    public async Task<EditProductResult> EditProduct(EditProductViewModel editProduct, IFormFile image)
    {
        var product = await _productRepository.GetProductById(editProduct.Id);
        if (product == null) return EditProductResult.NotFound;
        if (editProduct.ProductSelectedCategory == null) return EditProductResult.CategoryIsNull;

        #region edit product
        product.ShortDescription = editProduct.ShortDescription;
        product.Description = editProduct.Description;
        product.IsActive = editProduct.IsActive;
        product.Price = editProduct.Price;
        product.Name = editProduct.Name;


        if (image != null && image.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);

            image.AddImageToServer(imageName, PathExtensions.ProductOriginServer, 257, 273,
                PathExtensions.ProductThumbServer);

            product.ImageName = imageName;
        }
        #endregion
        _productRepository.UpdateProduct(product);

        await _productRepository.RemoveProductSelectCategory(editProduct.Id);
        await _productRepository.AddProductSelectCategory(editProduct.ProductSelectedCategory, editProduct.Id);


        await _productRepository.SaveChangeAsync();

        return EditProductResult.Success;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        return await _productRepository.DeleteProduct(productId);
    }

    public async Task<bool> RecoveryProduct(Guid productId)
    {
        return await _productRepository.RecoveryProduct(productId);
    }

    public async Task<bool> AddProductGallery(Guid productId, List<IFormFile> images)
    {
        if (!await _productRepository.CheckProduct(productId))
        {
            return false;
        }

        if (images != null && images.Any())
        {
            var productGallery = new List<ProductGallery>();
            foreach (var image in images)
            {
                if (image.IsImage())
                {
                    var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                    image.AddImageToServer(imageName, PathExtensions.ProductOriginServer, 255, 273, PathExtensions.ProductThumbServer);


                    productGallery.Add(new ProductGallery
                    {
                        ImageName = imageName,
                        ProductId = productId
                    });
                }
            }

            await _productRepository.AddProductGalleries(productGallery);

        }
        return true;
    }

    public async Task<List<ProductGallery>> GetAllProductGalleries(Guid productId)
    {
        return await _productRepository.GetAllProductGalleries(productId);
    }

    public async Task DeleteImage(Guid galleryId)
    {
        var productGallery = await _productRepository.GetProductGalleriesById(galleryId);
        if (productGallery != null)
        {
            UploadImageExtension.DeleteImage(productGallery.ImageName, PathExtensions.ProductOriginServer, PathExtensions.ProductThumbServer);
            await _productRepository.DeleteProductGallery(galleryId);
        }
    }
}