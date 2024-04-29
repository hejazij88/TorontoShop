
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Infa.Data.Context;

namespace TorontoShop.Infa.Data.Repository
{
    public class UserRepository: IUserRepository
    {
        #region constractor
        private readonly ShopDbContext _context;
        public UserRepository(ShopDbContext context)
        {
            _context = context;
        }
        #endregion

        #region user

        #endregion
    }
}
