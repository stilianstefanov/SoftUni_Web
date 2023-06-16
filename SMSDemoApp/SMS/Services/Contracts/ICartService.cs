namespace SMS.Services.Contracts;

using ViewModels;

public interface ICartService
{
    Task GenerateUserCartAsync(string userId);
    Task<IEnumerable<ProductAllViewModel>> GetCartProductsAsync(string userId);
    Task BuyCartProductsAsync(string userId);
}
