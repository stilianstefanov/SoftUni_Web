namespace BusStationApp.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels;

public class DestinationService : IDestinationService
{
    private readonly BusStationDbContext _dbContext;

    public DestinationService(BusStationDbContext busStationDbContext)
    {
        _dbContext = busStationDbContext;
    }


    public async Task<IEnumerable<DestinationAllViewModel>> GetAllDestinationsAsync()
    {
        IEnumerable<DestinationAllViewModel> destinations = await _dbContext
            .Destinations
            .Select(d => new DestinationAllViewModel()
            {
                DestinationName = d.DestinationName,
                Origin = d.Origin,
                ImageUrl = d.ImageUrl,
                DateAndTime = d.Date + " " + d.Time,
                TicketsCount = d.Tickets.Count(t => t.UserId == null),
                Id = d.Id
            })
            .ToListAsync();

        return destinations;
    }

    public async Task AddDestinationAsync(DestinationAddViewModel destinationAddViewModel)
    {
        Destination destination = new Destination()
        {
            DestinationName = destinationAddViewModel.DestinationName,
            Origin = destinationAddViewModel.Origin,
            Date = destinationAddViewModel.Date.ToString("MM/dd/yyyy"),
            Time = destinationAddViewModel.Date.ToString("hh:mm tt"),
            ImageUrl = destinationAddViewModel.ImageUrl
        };

        await _dbContext.Destinations.AddAsync(destination);
        await _dbContext.SaveChangesAsync();
    }
}
