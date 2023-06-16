namespace SMS.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

[Authorize]
public class CartsController : Controller
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<IActionResult> GenerateUserCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            await _cartService.GenerateUserCartAsync(userId);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            return RedirectToAction("Index", "Home");
        }
    }

    public async Task<IActionResult> Details()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartProducts = await _cartService.GetCartProductsAsync(userId);

            return View(cartProducts);
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "An unexpected error occurred!");

            return RedirectToAction("Index", "Home");
        }
    }

    public async Task<IActionResult> Buy()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _cartService.BuyCartProductsAsync(userId);

            return RedirectToAction("Index", "Home");
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "An unexpected error occurred!");

            return RedirectToAction("Index", "Home");
        }
    }
}
