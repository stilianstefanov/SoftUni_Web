using Library.Data.Models;

namespace Library.Services
{
    using System.Collections;
    using Microsoft.EntityFrameworkCore;

    using Library.Services.Contracts;
    using Library.ViewModels;
    using Library.Data;

    public class BookService : IBookService
    {
        private readonly LibraryDbContext _dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookAllViewModel>> GetAllAsync()
        {
            IEnumerable<BookAllViewModel> bookAllViewModels = await _dbContext
                .Books
                .Select(book => new BookAllViewModel()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Category = book.Category.Name,
                    Rating = book.Rating.ToString(),
                    Title = book.Title,
                    ImageUrl = book.ImageUrl
                })
                .ToListAsync();

            return bookAllViewModels;
        }

        public async Task<IEnumerable<BookMyViewModel>> GetMineBooksAsync(string userId)
        {
            IEnumerable<BookMyViewModel> bookMyViewModels = await _dbContext
                .Books
                .Where(b => b.UsersBooks.Any(ub => ub.CollectorId == userId))
                .Select(b => new BookMyViewModel()
                {
                    Id = b.Id,
                    Author = b.Author,
                    Category = b.Category.Name,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Title = b.Title
                })
                .ToListAsync();

            return bookMyViewModels;
        }

        public async Task<BookAddFormModel> GenerateFormModelAsync()
        {
           BookAddFormModel model = new BookAddFormModel();

           model.Categories = await _dbContext.Categories
               .Select(c => new CategorySelectViewModel()
               {
                   Id = c.Id,
                   Name = c.Name
               })
               .ToListAsync();

           return model;
        }

        public async Task AddBookAsync(BookAddFormModel inputModel, decimal rating)
        {
            Book book = new Book()
            {
                Author = inputModel.Author,
                CategoryId = inputModel.CategoryId,
                Description = inputModel.Description,
                ImageUrl = inputModel.ImageUrl,
                Rating = rating,
                Title = inputModel.Title
            };

            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddBookToCollectionAsync(int bookId, string userId)
        {
            bool isBookAlreadyAdded = _dbContext.UsersBooks
                .Any(ub => ub.CollectorId == userId && ub.BookId == bookId);

            if (!isBookAlreadyAdded)
            {
                IdentityUserBook identityUserBook = new IdentityUserBook()
                {
                    BookId = bookId,
                    CollectorId = userId
                };

                await _dbContext.UsersBooks.AddAsync(identityUserBook);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveBookFromCollectionAsync(int bookId, string userId)
        {
            IdentityUserBook identityUserBook = await _dbContext
                .UsersBooks
                .Where(ub => ub.BookId == bookId && ub.CollectorId == userId)
                .FirstAsync();

            _dbContext.UsersBooks.Remove(identityUserBook);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BookEditFormModel> GenerateEditFormModelAsync(int id)
        {
            BookEditFormModel model = await _dbContext
                .Books
                .Where(b => b.Id == id)
                .Select(b => new BookEditFormModel()
                {
                    Author = b.Author,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating.ToString(),
                    CategoryId = b.CategoryId
                })
                .FirstAsync();

            model.Categories = await _dbContext.Categories
                .Select(c => new CategorySelectViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return model;
        }

        public async Task EditAsync(int id, BookEditFormModel inputEditFormModel)
        {
            Book book = await _dbContext.Books
                .FirstAsync(b => b.Id == id);

            book.Author = inputEditFormModel.Author;
            book.Title = inputEditFormModel.Title;
            book.Description = inputEditFormModel.Description;
            book.ImageUrl = inputEditFormModel.ImageUrl;
            book.Rating = decimal.Parse(inputEditFormModel.Rating);
            book.CategoryId = inputEditFormModel.CategoryId;

            await _dbContext.SaveChangesAsync();
        }
    }
}