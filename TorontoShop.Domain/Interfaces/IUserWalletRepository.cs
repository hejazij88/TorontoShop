using TorontoShop.Domain.Model.Wallet;

namespace TorontoShop.Domain.Interfaces;

public interface IUserWalletRepository
{
    Task CreateWalletAsync(UserWallet wallet);

    Task SaveChangeAsync();
}