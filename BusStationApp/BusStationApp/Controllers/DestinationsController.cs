namespace BusStationApp.Controllers;

using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels;

[Authorize]
public class DestinationsController : Controller
{
    private readonly IDestinationService _destinationService;

    public DestinationsController(IDestinationService destinationService)
    {
        _destinationService = destinationService;
    }

    public async Task<IActionResult> All()
    {
        try
        {
            var destinations = await _destinationService.GetAllDestinationsAsync();

            return View(destinations);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Something went wrong.");

            return View();
        }
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(DestinationAddViewModel destinationAddViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(destinationAddViewModel);
        }

        try
        {
            await _destinationService.AddDestinationAsync(destinationAddViewModel);

            return RedirectToAction("All", "Destinations");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Something went wrong.");

            return View(destinationAddViewModel);
        }
    }
}
