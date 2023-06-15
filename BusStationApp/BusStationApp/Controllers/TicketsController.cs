namespace BusStationApp.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels;

[Authorize]
public class TicketsController : Controller
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TicketCreateViewModel ticketCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(ticketCreateViewModel);
        }

        try
        {
            await _ticketService.CreateTicketAsync(ticketCreateViewModel);

            return RedirectToAction("All", "Destinations");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Something went wrong.");

            return View(ticketCreateViewModel);
        }
    }

    public async Task<IActionResult> Reserve(int destinationId)
    {
        int freeTicketsCount = await _ticketService.GetFreeTicketsCountAsync(destinationId);

        if (freeTicketsCount == 0)
        {
            return RedirectToAction("All", "Destinations");
        }

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _ticketService.ReserveTicket(destinationId, userId);

        return RedirectToAction("All", "Destinations");
    }

    public async Task<IActionResult> MyTickets()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var tickets = await _ticketService.GetMyTicketsAsync(userId);

            return View(tickets);
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Something went wrong.");

            return View();
        }
    }
}
