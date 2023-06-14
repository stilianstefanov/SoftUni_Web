namespace FootballManagerApp.Services.Contracts;

using ViewModels;

public interface IPlayersService
{
    Task<IEnumerable<PlayerViewModel>> GetAllAsync();
    Task AddAsync(PlayerAddViewModel playerAddViewModel, string userId);
    Task<IEnumerable<PlayerViewModel>> GetMyPlayersAsync(string userId);
    Task AddToCollectionAsync(int id, string userId);
    Task RemoveFromCollectionAsync(int id, string userId);
}
