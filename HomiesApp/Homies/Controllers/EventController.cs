namespace Homies.Controllers;

using System.Security.Claims;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Services.Contracts;
using ViewModels.Event;

[Authorize]
public class EventController : Controller
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    public async Task<IActionResult> All()
    {
        try
        {
            var events = await _eventService.GetAllAsync();

            return View(events);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");

            return View();
        }
    }

    public async Task<IActionResult> Joined()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var events = await _eventService.GetJoinedAsync(userId);

            return View(events);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");

            return RedirectToAction("All");
        }
    }

    public async Task<IActionResult> Add()
    {
        var eventAddViewModel = await _eventService.GetAddViewModelAsync();

        return View(eventAddViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(EventAddViewModel eventAddViewModel)
    {
        if (!ModelState.IsValid)
        {
            eventAddViewModel = await _eventService.GetAddViewModelAsync();

            return View(eventAddViewModel);
        }

        bool isStartDateTimeValid = DateTime.TryParse(eventAddViewModel.Start, out DateTime startDateTime);
        bool isEndDateTimeValid = DateTime.TryParse(eventAddViewModel.End, out DateTime endDateTime);

        if (!isStartDateTimeValid || !isEndDateTimeValid)
        {
            eventAddViewModel = await _eventService.GetAddViewModelAsync();

            ModelState.AddModelError(string.Empty, "Invalid date time format.");

            return View(eventAddViewModel);
        }

        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _eventService.AddAsync(eventAddViewModel, userId);

            return RedirectToAction("All");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");

            eventAddViewModel = await _eventService.GetAddViewModelAsync();

            return View(eventAddViewModel);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var eventOrganiserId = await _eventService.GetOrganiserIdAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (eventOrganiserId != userId)
            {
                return RedirectToAction("All");
            }

            var eventEditViewModel = await _eventService.GetEditViewModelAsync(id);

            return View(eventEditViewModel);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");

            return RedirectToAction("All");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EventEditViewModel editEventViewModel)
    {
        if (!ModelState.IsValid)
        {
            editEventViewModel = await _eventService.GetEditViewModelAsync(editEventViewModel.Id);

            return View(editEventViewModel);
        }

        bool isStartDateTimeValid = DateTime.TryParse(editEventViewModel.Start, out DateTime startDateTime);
        bool isEndDateTimeValid = DateTime.TryParse(editEventViewModel.End, out DateTime endDateTime);

        if (!isStartDateTimeValid || !isEndDateTimeValid)
        {
            editEventViewModel = await _eventService.GetEditViewModelAsync(editEventViewModel.Id);

            ModelState.AddModelError(string.Empty, "Invalid date time format.");

            return View(editEventViewModel);
        }

        try
        {
            await _eventService.EditAsync(editEventViewModel);

            return RedirectToAction("All");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");

            return RedirectToAction("All");
        }
    }

    public async Task<IActionResult> Join(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isAlreadyJoined = await _eventService.IsAlreadyJoinedAsync(id, userId);

            if (isAlreadyJoined)
            {
                return RedirectToAction("All");
            }

            await _eventService.JoinAsync(id, userId);

            return RedirectToAction("Joined");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");

            return RedirectToAction("Joined");
        }
    }

    public async Task<IActionResult> Leave(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _eventService.LeaveAsync(id, userId);

            return RedirectToAction("All");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");

            return RedirectToAction("All");
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var eventDetailsViewModel = await _eventService.GetDetailsViewModelAsync(id);

            return View(eventDetailsViewModel);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");

            return RedirectToAction("All");
        }
    }
}
