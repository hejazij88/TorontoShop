using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.Model.Wallet;

namespace TorontoShop.Infa.Data.Context
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<UserWallet> Wallets { get; set; }

    }

}
