using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.Accounts;

namespace TorontoShop.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> IsUserPhoneExist(string phone);

        public Task RegisterUser(User user);

        public Task SaveChange();

        public Task <User?> GetUserByPhoneNumber(string phoneNumber);
    }
}
