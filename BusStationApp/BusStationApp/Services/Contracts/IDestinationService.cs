namespace BusStationApp.Services.Contracts;

using ViewModels;

public interface IDestinationService
{
    Task<IEnumerable<DestinationAllViewModel>> GetAllDestinationsAsync();
    Task AddDestinationAsync(DestinationAddViewModel destinationAddViewModel);
}
