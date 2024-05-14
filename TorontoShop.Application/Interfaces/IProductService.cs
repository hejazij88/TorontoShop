﻿using Microsoft.AspNetCore.Http;
using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.ViewModel.Admin.Product;

namespace TorontoShop.Application.Interfaces;

public interface IProductService
{
    Task<CreateProductCategoryResult> CreateProductCategory(CreateProductCategoryViewModel categoryViewModel, IFormFile file);
    Task<EditProductCategoryResult> EditProductCategory(EditProductCategoryViewModel categoryViewModel, IFormFile file);
    Task<EditProductCategoryViewModel>GetEditProductCategory(Guid  productCategoryId);
    Task<ProductCategoryFilterViewModel> ProductCategoryFilter(ProductCategoryFilterViewModel categoryFilterViewModel);
    Task<FilterProductViewModel> FilterProduct(FilterProductViewModel filterProductViewModel);
    Task<CreateProductResult> CreateProduct(CreateProductViewModel categoryViewModel, IFormFile image);
    Task<List<ProductCategory>> GetAllCategory();
    Task<EditProductViewModel> GetEditProduct(Guid productId);
    Task<EditProductResult> EditProduct(EditProductViewModel editProduct,IFormFile image);
    Task<bool> DeleteProduct(Guid productId);
    Task<bool> RecoveryProduct(Guid productId);


}