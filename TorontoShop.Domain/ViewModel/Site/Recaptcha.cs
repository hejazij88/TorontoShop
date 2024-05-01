using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorontoShop.Domain.ViewModel.Site
{
    public class Recaptcha
    {
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string Token { get; set; }
    }
}
