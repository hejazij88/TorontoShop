using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.ViewModel.Paging;


namespace TorontoShop.Domain.ViewModel.Admin.Account
{
    public class FilterUserViewModel:BasePaging
    {
        public string PhoneNumber { get; set; }
        public List<User> Users { get; set; }

        #region methods
        public FilterUserViewModel SetUsers(List<User> users)
        {
            this.Users = users;
            return this;
        }

        public FilterUserViewModel SetPaging(BasePaging paging)
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
