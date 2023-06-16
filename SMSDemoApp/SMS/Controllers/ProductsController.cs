using Microsoft.AspNetCore.Mvc;

namespace SMS.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Services.Contracts;
using ViewModels;

[Authorize]
public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        bool isPriceValid = decimal.TryParse(model.Price, out decimal price);

        if (!isPriceValid)
        {
            ModelState.AddModelError(nameof(model.Price), "Invalid price!");

            return View(model);
        }

        try
        {
            await _productService.CreateProductAsync(model);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An Unexpected error occurred!");

            return View(model);
        }
    }

    public async Task<IActionResult> Add(string id)
    {
        try
        {
            var product = await _productService.GetProductAsync(id);

            return View(product);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An Unexpected error occurred!");

            return RedirectToAction("Index", "Home");
        }
        
    }

    public async Task<IActionResult> AddToCart(string id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _productService.AddProductToCartAsync(id, userId);

            return RedirectToAction("Details", "Carts");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An Unexpected error occurred!");

            return RedirectToAction("Index", "Home");
        }
    }
}
