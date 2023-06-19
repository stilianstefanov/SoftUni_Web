namespace Homies.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels.Event;
using ViewModels.Type;

public class EventService : IEventService
{
    private readonly HomiesDbContext _dbContext;

    public EventService(HomiesDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<EventAllViewModel>> GetAllAsync()
    {
        IEnumerable<EventAllViewModel> events = await _dbContext
            .Events
            .Select(e => new EventAllViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Organiser = e.Organiser.UserName,
                Start = e.Start.ToString("yyyy-MM-dd H:mm"),
                Type = e.Type.Name
            })
            .ToListAsync();

        return events;
    }

    public async Task<IEnumerable<EventAllViewModel>> GetJoinedAsync(string userId)
    {
        IEnumerable<EventAllViewModel> events = await _dbContext
            .Events
            .Where(e => e.EventsParticipants.Any(ep => ep.HelperId == userId))
            .Select(e => new EventAllViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Organiser = e.Organiser.UserName,
                Start = e.Start.ToString("yyyy-MM-dd H:mm"),
                Type = e.Type.Name
            })
            .ToListAsync();

        return events;
    }

    public async Task<EventAddViewModel> GetAddViewModelAsync()
    {
        EventAddViewModel eventAddViewModel = new EventAddViewModel();

        eventAddViewModel.Types = await _dbContext
            .Types
            .Select(t => new TypeDropDownViewModel()
            {
                Name = t.Name,
                Id = t.Id
            })
            .ToListAsync();

        return eventAddViewModel;
    }

    public async Task AddAsync(EventAddViewModel eventAddViewModel, string userId)
    {
        Event newEvent = new Event()
        {
            Name = eventAddViewModel.Name,
            Start = DateTime.Parse(eventAddViewModel.Start),
            End = DateTime.Parse(eventAddViewModel.End),
            TypeId = eventAddViewModel.TypeId,
            OrganiserId = userId,
            Description = eventAddViewModel.Description,
            CreatedOn = DateTime.UtcNow
        };

        newEvent.EventsParticipants.Add(new EventParticipant()
        {
            HelperId = userId
        });

        await _dbContext.Events.AddAsync(newEvent);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<string> GetOrganiserIdAsync(int id)
    {
        string organiserId = await _dbContext
            .Events
            .Where(e => e.Id == id)
            .Select(e => e.OrganiserId)
            .FirstAsync();

        return organiserId;
    }

    public async Task<EventEditViewModel> GetEditViewModelAsync(int id)
    {
        EventEditViewModel eventEditViewModel = await _dbContext
            .Events
            .Where(e => e.Id == id)
            .Select(e => new EventEditViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Start = e.Start.ToString("yyyy-MM-dd H:mm"),
                End = e.End.ToString("yyyy-MM-dd H:mm"),
                TypeId = e.TypeId
            })
            .FirstAsync();

        eventEditViewModel.Types = await _dbContext
            .Types
            .Select(t => new TypeDropDownViewModel()
            {
                Name = t.Name,
                Id = t.Id
            })
            .ToListAsync();

        return eventEditViewModel;
    }

    public async Task EditAsync(EventEditViewModel editEventViewModel)
    {
        Event eventToEdit = await _dbContext
            .Events
            .Where(e => e.Id == editEventViewModel.Id)
            .FirstAsync();

        eventToEdit.Name = editEventViewModel.Name;
        eventToEdit.Description = editEventViewModel.Description;
        eventToEdit.Start = DateTime.Parse(editEventViewModel.Start);
        eventToEdit.End = DateTime.Parse(editEventViewModel.End);
        eventToEdit.TypeId = editEventViewModel.TypeId;

        await _dbContext.SaveChangesAsync();
    }

    public async Task JoinAsync(int id, string userId)
    {

        EventParticipant eventParticipant = new EventParticipant()
        {
            EventId = id,
            HelperId = userId
        };

        await _dbContext.EventsParticipants.AddAsync(eventParticipant);
        await _dbContext.SaveChangesAsync();

    }

    public async Task LeaveAsync(int id, string userId)
    {
        EventParticipant? eventParticipant = await _dbContext
            .EventsParticipants
            .Where(ep => ep.EventId == id && ep.HelperId == userId)
            .FirstOrDefaultAsync();

        if (eventParticipant != null)
        {
            _dbContext.EventsParticipants.Remove(eventParticipant);

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<EventDetailsViewModel> GetDetailsViewModelAsync(int id)
    {
        EventDetailsViewModel eventDetailsViewModel = await _dbContext
            .Events
            .Where(e => e.Id == id)
            .Select(e => new EventDetailsViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Start = e.Start.ToString("yyyy-MM-dd H:mm"),
                End = e.End.ToString("yyyy-MM-dd H:mm"),
                Type = e.Type.Name,
                Organiser = e.Organiser.UserName,
                CreatedOn = e.CreatedOn.ToString("yyyy-MM-dd H:mm")
            })
            .FirstAsync();

        return eventDetailsViewModel;
    }

    public async Task<bool> IsAlreadyJoinedAsync(int id, string userId)
    {
        bool isAlreadyJoined = await _dbContext
            .EventsParticipants
            .AnyAsync(ep => ep.EventId == id && ep.HelperId == userId);

        return isAlreadyJoined;
    }
}
