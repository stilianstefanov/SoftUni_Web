namespace FootballManagerApp.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels;

public class PlayersService : IPlayersService
{
    private readonly FootballManagerDbContext _dbContext;

    public PlayersService(FootballManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PlayerViewModel>> GetAllAsync()
    {
        IEnumerable<PlayerViewModel> allPlayers = await _dbContext
            .Players
            .Select(p => new PlayerViewModel()
            {
                Description = p.Description,
                Endurance = p.Endurance,
                FullName = p.FullName,
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Position = p.Position,
                Speed = p.Speed
            })
            .ToListAsync();

        return allPlayers;
    }

    public async Task AddAsync(PlayerAddViewModel playerAddViewModel, string userId)
    {
        Player player = new Player()
        {
            Description = playerAddViewModel.Description,
            Endurance = playerAddViewModel.Endurance,
            FullName = playerAddViewModel.FullName,
            ImageUrl = playerAddViewModel.ImageUrl,
            Position = playerAddViewModel.Position,
            Speed = playerAddViewModel.Speed
        };

        player.UserPlayers.Add(new UserPlayer()
        {
            UserId = userId
        });

        await _dbContext.Players.AddAsync(player);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<PlayerViewModel>> GetMyPlayersAsync(string userId)
    {
        IEnumerable<PlayerViewModel> myPlayers = await _dbContext
            .UsersPlayers
            .Where(up => up.UserId == userId)
            .Select(up => new PlayerViewModel()
            {
                Description = up.Player.Description,
                Endurance = up.Player.Endurance,
                FullName = up.Player.FullName,
                Id = up.Player.Id,
                ImageUrl = up.Player.ImageUrl,
                Position = up.Player.Position,
                Speed = up.Player.Speed
            })
            .ToListAsync();

        return myPlayers;
    }

    public async Task AddToCollectionAsync(int id, string userId)
    {
        bool isAlreadyAdded = await _dbContext
            .UsersPlayers
            .AnyAsync(up => up.PlayerId == id && up.UserId == userId);

        if (!isAlreadyAdded)
        {
            UserPlayer userPlayer = new UserPlayer()
            {
                PlayerId = id,
                UserId = userId
            };

            await _dbContext.UsersPlayers.AddAsync(userPlayer);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task RemoveFromCollectionAsync(int id, string userId)
    {
        UserPlayer? userPlayer = await _dbContext
            .UsersPlayers
            .FirstOrDefaultAsync(up => up.PlayerId == id && up.UserId == userId);

        if (userPlayer != null)
        {
            _dbContext.UsersPlayers.Remove(userPlayer);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<PlayerEditViewModel> GetPlayerForEditAsync(int id)
    {
        PlayerEditViewModel? playerEditViewModel = await _dbContext
            .Players
            .Where(p => p.Id == id)
            .Select(p => new PlayerEditViewModel()
            {
                Description = p.Description,
                Endurance = p.Endurance,
                FullName = p.FullName,
                ImageUrl = p.ImageUrl,
                Position = p.Position,
                Speed = p.Speed
            })
            .FirstAsync();

        return playerEditViewModel;
    }

    public async Task EditAsync(PlayerEditViewModel playerEditViewModel)
    {
        Player player = await _dbContext
            .Players
            .FirstAsync(p => p.Id == playerEditViewModel.Id);

        player.Description = playerEditViewModel.Description;
        player.Endurance = playerEditViewModel.Endurance;
        player.FullName = playerEditViewModel.FullName;
        player.ImageUrl = playerEditViewModel.ImageUrl;
        player.Position = playerEditViewModel.Position;
        player.Speed = playerEditViewModel.Speed;

        await _dbContext.SaveChangesAsync();
    }
}
