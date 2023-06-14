namespace Library.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Services.Contracts;
using ViewModels;

[Authorize]
public class BookController : Controller
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<IActionResult> All()
    {
        var viewModels = await _bookService.GetAllAsync();

        return View(viewModels);
    }

    public async Task<IActionResult> Mine()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var viewModels = await _bookService.GetMineBooksAsync(userId);

        return View(viewModels);
    }

    public async Task<IActionResult> Add()
    {
        var inputModel = await _bookService.GenerateFormModelAsync();

        return View(inputModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(BookAddFormModel inputModel)
    {
        if (!ModelState.IsValid)
        {
            inputModel = await _bookService.GenerateFormModelAsync();
            return View(inputModel);
        }

        decimal rating;
        var isValid = decimal.TryParse(inputModel.Rating, out rating);

        if (!isValid || rating < 0 || rating > 10)
        {
            ModelState.AddModelError(nameof(inputModel.Rating), "Rating must be between 0.00 and 10.00");

            inputModel = await _bookService.GenerateFormModelAsync();
            return View(inputModel);
        }

        await _bookService.AddBookAsync(inputModel, rating);

        return RedirectToAction("All", "Book");
    }

    [HttpPost]
    public async Task<IActionResult> AddToCollection(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _bookService.AddBookToCollectionAsync(id, userId);

        return RedirectToAction("All", "Book");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCollection(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _bookService.RemoveBookFromCollectionAsync(id, userId);

        return RedirectToAction("Mine", "Book");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var inputModel = await _bookService.GenerateEditFormModelAsync(id);

        return View(inputModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, BookEditFormModel inputEditFormModel)
    {
        if (!ModelState.IsValid)
        {
            var inputModel = await _bookService.GenerateEditFormModelAsync(id);

            return View(inputModel);
        }

        await _bookService.EditAsync(id, inputEditFormModel);

        return RedirectToAction("All", "Book");
    }
}
