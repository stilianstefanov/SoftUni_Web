using Microsoft.AspNetCore.Mvc;

namespace SMS.Controllers;

using Microsoft.AspNetCore.Authorization;
using Services.Contracts;

[Authorize]
public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("IndexLoggedIn");
        }

        return View();
    }

    public async Task<IActionResult> IndexLoggedIn()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();

            return View(products);
        }
        catch (Exception)
        {
           ModelState.AddModelError(string.Empty, "An unexpected error occurred");

           return View();
        }
    }
}
