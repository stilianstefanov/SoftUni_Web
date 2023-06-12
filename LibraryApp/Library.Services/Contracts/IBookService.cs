namespace Library.Services.Contracts
{
    using Library.ViewModels;

    public interface IBookService
    {
        Task<IEnumerable<BookAllViewModel>> GetAllAsync();
        Task<IEnumerable<BookMyViewModel>> GetMineBooksAsync(string userId);
        Task<BookAddFormModel> GenerateFormModelAsync();
        Task AddBookAsync(BookAddFormModel inputModel, decimal rating);
        Task AddBookToCollectionAsync(int bookId, string userId);
        Task RemoveBookFromCollectionAsync(int bookId, string userId);
        Task<BookEditFormModel> GenerateEditFormModelAsync(int id);
        Task EditAsync(int id, BookEditFormModel inputEditFormModel);
    }
}
