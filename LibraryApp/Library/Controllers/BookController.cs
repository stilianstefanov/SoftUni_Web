namespace Library.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Collections;
    using System.Security.Claims;

    using Library.Services.Contracts;
    using Library.ViewModels;


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
            IEnumerable<BookAllViewModel> viewModels = await _bookService.GetAllAsync();

            return View(viewModels);
        }

        public async Task<IActionResult> Mine()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IEnumerable<BookMyViewModel> viewModels = await _bookService.GetMineBooksAsync(userId);

            return View(viewModels);
        }

        public async Task<IActionResult> Add()
        {
            BookAddFormModel inputModel = await _bookService.GenerateFormModelAsync();

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
            bool isValid = decimal.TryParse(inputModel.Rating, out rating);

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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _bookService.AddBookToCollectionAsync(id, userId );

            return RedirectToAction("All", "Book");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _bookService.RemoveBookFromCollectionAsync(id, userId );

            return RedirectToAction("Mine", "Book");
        }

        public async Task<IActionResult> Edit(int id)
        {
            BookEditFormModel inputModel = await _bookService.GenerateEditFormModelAsync(id);

            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookEditFormModel inputEditFormModel)
        {
            if (!ModelState.IsValid)
            {
                BookEditFormModel inputModel = await _bookService.GenerateEditFormModelAsync(id);

                return View(inputModel);
            }

            await _bookService.EditAsync(id, inputEditFormModel);

            return RedirectToAction("All", "Book");
        }
    }
}
