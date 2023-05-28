namespace MVC_Intro.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Seeding;

    using ViewModels.Product;

    public class ProductController : Controller
    {
        private readonly ICollection<ProductViewModel> _products = ProductInitializer._products;

        [HttpGet]
        public IActionResult All()
        {
            return View(_products);
        }

        [HttpPost]
        public IActionResult All(string keyword = "")
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("All");
            }

            var foundProducts = _products.Where(p => p.Name.Contains(keyword)).ToList();

            return View(foundProducts);
        }

        public IActionResult GetProduct(string id)
        {
            var product = _products.FirstOrDefault(p => p.Id.ToString() == id);

            if (product == null)
            {
                return RedirectToAction("All");
            }

            return View(product);
        }


        [HttpPost]
        public IActionResult Add(ProductViewModel newProduct)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("All");
            }

            newProduct.Id = Guid.NewGuid();

            _products.Add(newProduct);

            return RedirectToAction("All");
        }
    }
}
