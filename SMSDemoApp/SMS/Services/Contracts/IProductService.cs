namespace SMS.Services.Contracts;

using ViewModels;

public interface IProductService
{
    Task<IEnumerable<ProductAllViewModel>> GetAllProductsAsync();
    Task CreateProductAsync(ProductCreateViewModel model);
    Task<ProductAllViewModel> GetProductAsync(string id);
    Task AddProductToCartAsync(string id, string userId);
}
