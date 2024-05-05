using TorontoShop.Domain.Model.Wallet;

namespace TorontoShop.Domain.Interfaces;

public interface IUserWalletRepository
{
    Task CreateWalletAsync(UserWallet wallet);

    Task<UserWallet> GetUserWalletById(Guid walletId);
    void UpdateWallet(UserWallet wallet);


    Task SaveChangeAsync();
}