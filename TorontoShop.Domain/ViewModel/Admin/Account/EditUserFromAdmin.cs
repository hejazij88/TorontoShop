using TorontoShop.Domain.Model.Accounts;
using System.ComponentModel.DataAnnotations;


namespace TorontoShop.Domain.ViewModel.Admin.Account
{
    public class EditUserFromAdmin
    {
        public long Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }

        [Display(Name = "شماره تلفن همراه")]
        public string PhoneNumber { get; set; }

        [Display(Name = "جنسیت")]
        public Gender UserGender { get; set; }


        [Display(Name = "گذرواژه")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Password { get; set; }
    }

    public enum EditUserFromAdminResult
    {
        NotFound,
        Success
    }
}
