﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.ViewModel.Wallet;

namespace TorontoShop.Application.Interfaces
{
    public interface IUserWalletService
    {
        Task<Guid> ChargeWalletAsync(Guid userId,ChargeUserWalletViewModel chargeUserWalletViewModel,string description);
    }
}
