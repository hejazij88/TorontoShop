using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.Wallet;
using TorontoShop.Domain.ViewModel.Wallet;

namespace TorontoShop.Application.Interfaces
{
    public interface IUserWalletService
    {
        Task<Guid> ChargeWalletAsync(Guid userId,ChargeUserWalletViewModel chargeUserWalletViewModel,string description);
        Task<bool> UpdateWalletForCharge(UserWallet wallet);


        Task<UserWallet> GetUserWalletById(Guid walletId);
    }
}
