using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorontoShop.Domain.ViewModel.Site.Slider
{
    public class CreateSliderViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int Price { get; set; }
        public string Href { get; set; }
        public string ButtonText { get; set; }
    }

    public enum CreateSliderResult
    {
        Success
    }
}
