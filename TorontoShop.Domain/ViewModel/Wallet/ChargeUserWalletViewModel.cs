using System.ComponentModel.DataAnnotations;

namespace TorontoShop.Domain.ViewModel.Wallet;

public class ChargeUserWalletViewModel
{
    [Display(Name = "مبلغ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int Amount { get; set; }
}