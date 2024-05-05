using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TorontoShop.Application.Extension
{
    public static class CommonExtension
    {
        public static string GetEnumName(this System.Enum dataEnum)
        {
            var enumDisplayName = dataEnum.GetType().GetMember(dataEnum.ToString()).FirstOrDefault();
            if (enumDisplayName != null)
            {
                return enumDisplayName.GetCustomAttribute<DisplayAttribute>()?.GetName();
            }
            return "";
        }
    }
}
