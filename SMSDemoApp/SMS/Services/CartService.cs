namespace SMS.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels;

public class CartService : ICartService
{
    private readonly SMSDbContext _dbContext;

    public CartService(SMSDbContext context)
    {
        _dbContext = context;
    }

    public async Task GenerateUserCartAsync(string userId)
    {
        Cart cart = new Cart()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId
        };

        await _dbContext.Carts.AddAsync(cart);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductAllViewModel>> GetCartProductsAsync(string userId)
    {
        IEnumerable<ProductAllViewModel> products = await _dbContext
            .Products
            .Where(p => p.Cart != null && p.Cart.UserId == userId)
            .Select(p => new ProductAllViewModel()
            {
                Name = p.Name,
                Price = p.Price.ToString("f2")
            })
            .ToListAsync();

        return products;
    }

    public async Task BuyCartProductsAsync(string userId)
    {
        Cart cart = await _dbContext.Carts
            .Include(c => c.Products)
            .FirstAsync(c => c.UserId == userId);

        cart.Products.Clear();
        await _dbContext.SaveChangesAsync();
    }
}
