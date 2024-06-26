﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TorontoShop.Application.Interfaces;
using TorontoShop.Application.Services;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Infa.Data.Repository;

namespace TorontoShop.InfaIoC
{
    public class DependencyContainer
    {

        public static void RegisterServices(IServiceCollection services)
        {


            #region Services
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IUserWalletService, UserWalletService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISliderService, SliderService>();



            #endregion


            #region Reposirory
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserWalletRepository, UserWalletRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();



            #endregion


            #region Tools
            services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddScoped<ISmsService, SmsService>();


            #endregion
        }
    }
}
