﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.Model.ProductEntity;
using TorontoShop.Domain.Model.Site;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductSelectedCategory> ProductSelectedCategory { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductFuture> ProductsFutures { get; set; }
        public DbSet<ProductGallery> Gallery { get; set; }
        public DbSet<Slide> Slides { get; set; }








    }

}
