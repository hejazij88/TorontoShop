using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.Wallet;
using TorontoShop.Infa.Data.Context;

namespace TorontoShop.Infa.Data.Repository;

public class UserWalletRepository:IUserWalletRepository
{
    private readonly ShopDbContext _context;
    public UserWalletRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task CreateWalletAsync(UserWallet wallet)
    {
        await _context.Wallets.AddAsync(wallet);
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }
}