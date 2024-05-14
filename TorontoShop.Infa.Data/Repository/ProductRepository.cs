using Microsoft.EntityFrameworkCore;
using System;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.ViewModel.Admin.Product;
using TorontoShop.Domain.ViewModel.Paging;
using TorontoShop.Infa.Data.Context;

namespace TorontoShop.Infa.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ShopDbContext _context;

    public ProductRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckUrlNameCategory(string url)
    {
        return await _context.ProductCategory.AsQueryable().AnyAsync(category => category.UrlName == url);
    }

    public async Task AddProductCategory(ProductCategory productCategory)
    {
        await _context.ProductCategory.AddAsync(productCategory);
    }

    public async Task<bool> CheckUrlNameCategories(string urlName, Guid CategoryId)
    {
        return await _context.ProductCategory.AsQueryable().AnyAsync(category => category.UrlName == urlName && category.Id != CategoryId);
    }

    public async Task<ProductCategory> GetProductCategoryById(Guid id)
    {
        return await _context.ProductCategory.AsQueryable().SingleOrDefaultAsync(category => category.Id == id);
    }

    public void UpdateProductCtaegory(ProductCategory category)
    {
        _context.Update(category);
    }

    public async Task<ProductCategoryFilterViewModel> FilterProductCategories(ProductCategoryFilterViewModel filter)
    {
        var query = _context.ProductCategory.AsQueryable();

        #region filter
        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(c => EF.Functions.Like(c.Title, $"%{filter.Title}%"));
        }
        #endregion

        #region paging
        var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
        var allData = await query.Paging(pager).ToListAsync();
        #endregion

        return filter.SetPaging(pager).SetCategory(allData);
    }

    public async Task<FilterProductViewModel> FilterProduct(FilterProductViewModel filterProductViewModel)
    {
        var query = _context.Product.Include(product => product.ProductSelectedCategory)
            .ThenInclude(category => category.ProductCategory).AsQueryable();

        #region Filter

        if (!string.IsNullOrEmpty(filterProductViewModel.ProductName))
        {
            query = query.Where(product => EF.Functions.Like(product.Name, $"%{filterProductViewModel.ProductName}%"));
        }

        if (!string.IsNullOrEmpty(filterProductViewModel.ProductCategoryName))
        {
            query = query.Where(product => product.ProductSelectedCategory.Any(category =>
                category.ProductCategory.UrlName == filterProductViewModel.ProductCategoryName));
        }


        switch (filterProductViewModel.State)
        {
            case ProductState.All:
                query = query.Where(product => !product.IsDeleted);
                break;
            case ProductState.IsActive:
                query = query.Where(product => product.IsActive);
                break;
            case ProductState.Delete:
                query = query.Where(product => product.IsDeleted);
                break;
        }

        switch (filterProductViewModel.ProductOrder)
        {
            case ProductOrder.All:
                break;
            case ProductOrder.Chip:
                query = query.Where(product => product.IsActive).OrderBy(product => product.Price);
                break;
            case ProductOrder.Expensive:
                query = query.Where(product => product.IsActive).OrderByDescending(product => product.Price);
                break;
            case ProductOrder.News:
                query = query.Where(product => product.IsActive).OrderByDescending(product => product.CreatedDate);
                break;
        }

        #endregion

        var pager = Pager.Build(filterProductViewModel.PageId, await query.CountAsync(), filterProductViewModel.TakeEntity, filterProductViewModel.CountForShowAfterAndBefor);
        var allData = await query.Paging(pager).ToListAsync();

        return filterProductViewModel.SetPaging(pager).SetProduct(allData);
    }

    public async Task AddProduct(Product product)
    {
        await _context.AddAsync(product);
    }

    public async Task RemoveProductSelectCategory(Guid productId)
    {
        var allProductSelctedCategory = await _context.ProductSelectedCategory.AsQueryable()
            .Where(category => category.ProductId == productId).ToListAsync();


        if (allProductSelctedCategory.Any())
        {
            _context.ProductSelectedCategory.RemoveRange(allProductSelctedCategory);
        }
    }

    public async Task AddProductSelectCategory(List<Guid> productSelectCategory, Guid productId)
    {
        if (productSelectCategory != null && productSelectCategory.Any())
        {
            var newProductSelectedCategory = new List<ProductSelectedCategory>();

            foreach (var categoryId in productSelectCategory)
            {
                newProductSelectedCategory.Add(new ProductSelectedCategory
                {
                    ProductId = productId,
                    CategoryId = categoryId
                });
            }
            await _context.ProductSelectedCategory.AddRangeAsync(newProductSelectedCategory);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<ProductCategory>> GetAllCategory()
    {
        return await _context.ProductCategory.AsQueryable()
            .Where(category => !category.IsDeleted).ToListAsync();
    }

    public async Task<Product> GetProductById(Guid productId)
    {
        return await _context.Product.AsQueryable()
            .SingleOrDefaultAsync(product => product.Id == productId);
    }

    public async void UpdateProduct(Product product)
    {
        _context.Product.Update(product);
    }

    public async Task<List<Guid>> GetAllProductCategoriesId(Guid productId)
    {
        return await _context.ProductSelectedCategory.AsQueryable()
            .Where(category => category.ProductId == productId)
            .Select(category => category.CategoryId)
            .ToListAsync();
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        var product = await _context.Product.AsQueryable().Where(p => p.Id == productId).FirstOrDefaultAsync();
        if (product != null)
        {
            product.IsDeleted=true;
             _context.Product.Update(product);
             await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> RecoveryProduct(Guid productId)
    {
        var product = await _context.Product.AsQueryable().Where(p => p.Id == productId).FirstOrDefaultAsync();
        if (product != null)
        {
            product.IsDeleted = false;
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task AddProductGalleries(List<ProductGallery> productGalleries)
    {
        await _context.Gallery.AddRangeAsync(productGalleries);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckProduct(Guid productId)
    {
        return await _context.Product.AsQueryable()
            .AnyAsync(c => c.Id == productId);
    }
}