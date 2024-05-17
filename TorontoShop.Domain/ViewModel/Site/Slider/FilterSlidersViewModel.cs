using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.Site;
using TorontoShop.Domain.ViewModel.Admin.Product;
using TorontoShop.Domain.ViewModel.Paging;

namespace TorontoShop.Domain.ViewModel.Site.Slider
{
    public class FilterSlidersViewModel : BasePaging
    {
        public string Tiltle { get; set; }

        public List<Slide> Slides { get; set; }


        public FilterSlidersViewModel SetSlider(List<Slide> slides)
        {
            Slides = slides;
            return this;
        }

        public FilterSlidersViewModel SetPaging(BasePaging paging)
        {
            PageId = paging.PageId;
            AllEntityCount = paging.AllEntityCount;
            StartPage = paging.StartPage;
            EndPage = paging.EndPage;
            TakeEntity = paging.TakeEntity;
            CountForShowAfterAndBefor = paging.CountForShowAfterAndBefor;
            SkipEntity = paging.SkipEntity;
            PageCount = paging.PageCount;

            return this;
        }
    }
}
