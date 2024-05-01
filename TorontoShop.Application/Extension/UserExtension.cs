using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.Accounts;

namespace TorontoShop.Application.Extension
{
    public static class UserExtension
    {
        public static string GetUserName(this User user)
        {
            return $"{user.FirstName} {user.LastName}";
        }
    }
}
