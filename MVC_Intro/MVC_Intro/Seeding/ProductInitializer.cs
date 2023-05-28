using MVC_Intro.ViewModels.Product;

namespace MVC_Intro.Seeding
{
    public static class ProductInitializer
    {
        public static ICollection<ProductViewModel> _products = new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = Guid.NewGuid(),
                Name = "Eggs",
                Price = 2.99
            },
            new ProductViewModel()
            {
                Id = Guid.NewGuid(),
                Name = "Water",
                Price = 3.99
            },
            new ProductViewModel()
            {
                Id = Guid.NewGuid(),
                Name = "Cola",
                Price = 1.99
            }
        };

        public static void AddProduct(ProductViewModel productViewModel) 
             => _products.Add(productViewModel);
    }
}
