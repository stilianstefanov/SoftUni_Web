namespace BusStationApp.Services.Contracts;

using ViewModels;

public interface ITicketService
{
    Task CreateTicketAsync(TicketCreateViewModel ticketCreateViewModel);
    Task<int> GetFreeTicketsCountAsync(int destinationId);
    Task ReserveTicket(int destinationId, string userId);
    Task<IEnumerable<TicketMyViewModel>> GetMyTicketsAsync(string userId);
}
