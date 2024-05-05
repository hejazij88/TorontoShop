using System.Collections.Generic;
using TorontoShop.Domain.Model.Wallet;
using TorontoShop.Domain.ViewModel.Paging;

namespace TorontoShop.Domain.ViewModel.Wallet
{
    public class FilterWalletViewModel: BasePaging
    {
        #region properties
        public Guid? UserId { get; set; }
        public List<UserWallet> UserWallets { get; set; }
        #endregion

        #region methods
        public FilterWalletViewModel SetWallets(List<UserWallet> userWallets)
        {
            this.UserWallets = userWallets;
            return this;
        }

        public FilterWalletViewModel SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntityCount = paging.AllEntityCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.CountForShowAfterAndBefor = paging.CountForShowAfterAndBefor;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;

            return this;    
        }

        #endregion
    }
}
