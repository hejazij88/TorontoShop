using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.BaseEntities;

namespace TorontoShop.Domain.Model.Site
{
    public class Slide:BaseEntity
    {
        public string ImageName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Price { get; set; }
        public string Href { get; set; }
        public string ButtonText { get; set; }
    }
}
