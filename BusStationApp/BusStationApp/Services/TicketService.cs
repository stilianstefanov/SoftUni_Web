namespace BusStationApp.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels;

public class TicketService : ITicketService
{
    private readonly BusStationDbContext _dbContext;

    public TicketService(BusStationDbContext busStationDbContext)
    {
        _dbContext = busStationDbContext;
    }


    public async Task CreateTicketAsync(TicketCreateViewModel ticketCreateViewModel)
    {
        ICollection<Ticket> tickets = new List<Ticket>();

        for (int i = 0; i < ticketCreateViewModel.TicketsCount; i++)
        {
            Ticket ticket = new Ticket()
            {
                DestinationId = ticketCreateViewModel.DestinationId,
                Price = decimal.Parse(ticketCreateViewModel.Price)
            };

            tickets.Add(ticket);
        }

        await _dbContext.Tickets.AddRangeAsync(tickets);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> GetFreeTicketsCountAsync(int destinationId)
    {
        int count = await _dbContext
            .Tickets
            .CountAsync(t => t.DestinationId == destinationId && t.UserId == null);

        return count;
    }

    public async Task ReserveTicket(int destinationId, string userId)
    {
        Ticket ticket = await _dbContext
            .Tickets
            .FirstAsync(t => t.DestinationId == destinationId && t.UserId == null);

        ticket.UserId = userId;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TicketMyViewModel>> GetMyTicketsAsync(string userId)
    {
        IEnumerable<TicketMyViewModel> tickets = await _dbContext
            .Tickets
            .Where(t => t.UserId == userId)
            .Select(t => new TicketMyViewModel()
            {
                DestinationName = t.Destination.DestinationName,
                Origin = t.Destination.Origin,
                DateAndTime = t.Destination.Date + " " + t.Destination.Time,
                Price = t.Price.ToString("F2"),
                ImageUrl = t.Destination.ImageUrl
            })
            .ToListAsync();

        return tickets;
    }
}
