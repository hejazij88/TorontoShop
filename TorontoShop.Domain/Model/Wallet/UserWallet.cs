﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.Model.BaseEntities;

namespace TorontoShop.Domain.Model.Wallet
{
    public class UserWallet:BaseEntity
    {
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Guid UserId { get; set; }

        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public WalletType WalletType { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "شرح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "پرداخت شده / نشده")]
        public bool IsPay { get; set; }

        #region relations
        public User User { get; set; }
        #endregion

    }
    public enum WalletType
    {
        [Display(Name = "واریز")]
        Variz = 1,
        [Display(Name = "برداشت")]
        Bardasht = 2
    }
}
