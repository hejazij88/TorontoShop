using Microsoft.EntityFrameworkCore;
using System;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.ViewModel.Admin.Product;
using TorontoShop.Domain.ViewModel.Paging;
using TorontoShop.Infa.Data.Context;

namespace TorontoShop.Infa.Data.Repository;

public class ProductRepository:IProductRepository
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
        return await _context.ProductCategories.AsQueryable().AnyAsync(category =>category.UrlName==url);
    }

    public async Task AddProductCategory(ProductCategory productCategory)
    {
        await _context.ProductCategories.AddAsync(productCategory);
    }

    public async Task<bool> CheckUrlNameCategories(string urlName, Guid CategoryId)
    {
        return await _context.ProductCategories.AsQueryable().AnyAsync(category => category.UrlName == urlName&&category.Id!=CategoryId);
    }

    public async Task<ProductCategory> GetProductCategoryById(Guid id)
    {
        return await _context.ProductCategories.AsQueryable().SingleOrDefaultAsync(category => category.Id == id);
    }

    public void UpdateProductCtaegory(ProductCategory category)
    {
        _context.Update(category);
    }

    public async Task<ProductCategoryFilterViewModel> FilterProductCategories(ProductCategoryFilterViewModel filter)
    {
        var query = _context.ProductCategories.AsQueryable();

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
}