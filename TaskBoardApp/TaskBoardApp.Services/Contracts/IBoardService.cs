namespace TaskBoardApp.Services.Contracts
{
    using Web.ViewModels.Board;

    public interface IBoardService
    {
        Task<ICollection<BoardAllViewModel>> GetAllAsync();

        Task<IEnumerable<BoardSelectViewModel>> GetAllBoardsForSelectAsync();

        Task<bool> BoardExistByIdAsync(int id);
    }
}
