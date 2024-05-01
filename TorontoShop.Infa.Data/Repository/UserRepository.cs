
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.Accounts;
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

        public async Task<bool> IsUserPhoneExist(string phone)
        {
            return await _context.Users.AsQueryable().AnyAsync(user => user.PhoneNumber == phone);
        }

        public async Task RegisterUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();

        }

        public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
        {
           return await _context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
    }
}
