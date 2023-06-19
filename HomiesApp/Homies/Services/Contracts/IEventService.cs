namespace Homies.Services.Contracts;

using ViewModels.Event;

public interface IEventService
{
    Task<IEnumerable<EventAllViewModel>> GetAllAsync();
    Task<IEnumerable<EventAllViewModel>> GetJoinedAsync(string userId);
    Task<EventAddViewModel> GetAddViewModelAsync();
    Task AddAsync(EventAddViewModel eventAddViewModel, string userId);
    Task<string> GetOrganiserIdAsync(int id);
    Task<EventEditViewModel> GetEditViewModelAsync(int id);
    Task EditAsync(EventEditViewModel editEventViewModel);
    Task JoinAsync(int id, string userId);
    Task LeaveAsync(int id, string userId);
    Task<EventDetailsViewModel> GetDetailsViewModelAsync(int id);
    Task<bool> IsAlreadyJoinedAsync(int id, string userId);
}
