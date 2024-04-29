using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorontoShop.Domain.ViewModel.Accounts
{
    public class LogInViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PhoneNumber { get; set; }
        [Display(Name = " رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Password { get; set; }

        public bool IsRememberMe { get; set; }
    }

    public enum LogInUserStatus
    {
        NotFound,
        NoActive,
        Success,
        IsBlocked
    }

}
