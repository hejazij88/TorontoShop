using System;
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

            #endregion


            #region Reposirory
            services.AddScoped<IUserRepository, UserRepository>();

            #endregion


            #region Tools
            services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddScoped<ISmsService, SmsService>();


            #endregion
        }
    }
}
