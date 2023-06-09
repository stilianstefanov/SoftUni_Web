namespace TaskBoardApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Contracts;
    using Web.ViewModels.Board;
    using Web.ViewModels.Task;

    public class BoardService : IBoardService
    {
        private readonly TaskBoardDbContext _dbContext;

        public BoardService(TaskBoardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<BoardAllViewModel>> GetAllAsync()
        {
            ICollection<BoardAllViewModel> allBoards = await _dbContext
                .Boards
                .Select(b => new BoardAllViewModel()
                {
                    Name = b.Name,
                    Tasks = b.Tasks.Select(t => new TaskViewModel()
                        {
                            Id = t.Id.ToString(),
                            Description = t.Description,
                            Title = t.Title,
                            Owner = t.Owner.UserName
                        })
                        .ToList()
                })
                .ToListAsync();

            return allBoards;
        }

        public async Task<IEnumerable<BoardSelectViewModel>> GetAllBoardsForSelectAsync()
        {
            IEnumerable<BoardSelectViewModel> allBoards = await _dbContext
                .Boards
                .Select(b => new BoardSelectViewModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToArrayAsync();

            return allBoards;
        }

        public async Task<bool> BoardExistByIdAsync(int id)
        {
            return await _dbContext.Boards.AnyAsync(b => b.Id == id);
        }
    }
}