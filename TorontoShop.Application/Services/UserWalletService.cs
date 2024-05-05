using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.Wallet;
using TorontoShop.Domain.ViewModel.Wallet;

namespace TorontoShop.Application.Services
{
    public class UserWalletService:IUserWalletService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserWalletRepository _userWalletRepository;


        public UserWalletService(IUserRepository userRepository, IUserWalletRepository userWalletRepository)
        {
            _userRepository = userRepository;
            _userWalletRepository = userWalletRepository;
        }

        public async Task<Guid> ChargeWalletAsync(Guid userId, ChargeUserWalletViewModel chargeUserWalletViewModel, string description)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return Guid.Empty;

            var wallet = new UserWallet
            {
                UserId = userId,
                Amount = chargeUserWalletViewModel.Amount,
                Description = description,
                IsPay = false,
                WalletType = WalletType.Variz
            };
            await _userWalletRepository.CreateWalletAsync(wallet);
            await _userWalletRepository.SaveChangeAsync();
            return wallet.Id;
        }

        public async Task<bool> UpdateWalletForCharge(UserWallet wallet)
        {
            if (wallet != null)
            {
                wallet.IsPay = true;
                _userWalletRepository.UpdateWallet(wallet);
                await _userWalletRepository.SaveChangeAsync();
                return true;
            }
            return false;
        }

        public async Task<UserWallet> GetUserWalletById(Guid walletId)
        {
            return await _userWalletRepository.GetUserWalletById(walletId);
        }
    }
}
