using Microsoft.EntityFrameworkCore;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.Model.Site;
using TorontoShop.Domain.ViewModel.Paging;
using TorontoShop.Domain.ViewModel.Site.Slider;
using TorontoShop.Infa.Data.Context;

namespace TorontoShop.Infa.Data.Repository;

public class SliderRepository:ISliderRepository
{

    private readonly ShopDbContext _context;

    public SliderRepository(ShopDbContext context)
    {
        _context = context;
    }
    public async Task<FilterSlidersViewModel> FilterSliders(FilterSlidersViewModel filterSlidersViewModel)
    {
        var query = _context.Slides.AsQueryable();

        #region filter
        if (!string.IsNullOrEmpty(filterSlidersViewModel.Tiltle))
        {
            query = query.Where(c => EF.Functions.Like(c.Title, $"%{filterSlidersViewModel.Tiltle}%"));
        }
        #endregion

        #region paging
        var pager = Pager.Build(filterSlidersViewModel.PageId, await query.CountAsync(), filterSlidersViewModel.TakeEntity, filterSlidersViewModel.CountForShowAfterAndBefor);
        var allData = await query.Paging(pager).ToListAsync();
        #endregion

        return filterSlidersViewModel.SetPaging(pager).SetSlider(allData);
    }

    public async Task<Slide> GetSlide(Guid slideId)
    {
        return await _context.Slides.AsQueryable()
            .SingleOrDefaultAsync(slide => slide.Id == slideId);
    }   

    public async Task<bool> DeleteSlide(Guid slideId)
    {
        var slider = await _context.Slides.AsQueryable().Where(slide   => slide.Id == slideId).FirstOrDefaultAsync();
        if (slider != null)
        {
            slider.IsDeleted = true;
            _context.Slides.Update(slider);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public void UpdateSlide(Slide slide)
    {
        _context.Slides.Update(slide);
    }

    public async Task AddSlide(Slide slide)
    {
        await _context.Slides.AddAsync(slide);
    }

    public async Task SaveChange()
    {
        await _context.SaveChangesAsync();
    }
}