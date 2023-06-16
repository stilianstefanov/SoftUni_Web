namespace SMS.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels;

public class ProductService : IProductService
{
    private readonly SMSDbContext _dbContext;

    public ProductService(SMSDbContext context)
    {
        _dbContext = context;
    }


    public async Task<IEnumerable<ProductAllViewModel>> GetAllProductsAsync()
    {
        IEnumerable<ProductAllViewModel> products = await _dbContext
            .Products
            .Select(p => new ProductAllViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price.ToString("f2")
            })
            .ToListAsync();

        return products;
    }

    public async Task CreateProductAsync(ProductCreateViewModel model)
    {
        Product product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = model.Name,
            Price = decimal.Parse(model.Price)
        };

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProductAllViewModel> GetProductAsync(string id)
    {
        ProductAllViewModel product = await _dbContext
            .Products
            .Where(p => p.Id == id)
            .Select(p => new ProductAllViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price.ToString("f2")
            })
            .FirstAsync();

        return product;
    }

    public async Task AddProductToCartAsync(string id, string userId)
    {
        Product product = await _dbContext
            .Products
            .Where(p => p.Id == id)
            .FirstAsync();

        string cartId = await _dbContext
            .Carts
            .Where(c => c.UserId == userId)
            .Select(c => c.Id)
            .FirstAsync();

        product.CartId = cartId;

        await _dbContext.SaveChangesAsync();
    }
}
