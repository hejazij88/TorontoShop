using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorontoShop.Application.Interfaces
{
    public interface ISmsService
    {
        public Task SendVerificationCode(string phone, string activeCode);
    }
}
