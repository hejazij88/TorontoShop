using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavenegar;
using TorontoShop.Application.Interfaces;

namespace TorontoShop.Application.Services
{
    public class SmsService:ISmsService
    {
        private string apiKey = "";
        public async Task SendVerificationCode(string phone, string activeCode)
        {
            Kavenegar.KavenegarApi api = new KavenegarApi(apiKey);

            await api.VerifyLookup(phone,activeCode,"Verify");

        }
    }
}
