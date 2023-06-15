namespace FootballManagerApp.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using ViewModels;

[Authorize]
public class PlayersController : Controller
{
    private readonly IPlayersService _playersService;
    public PlayersController(IPlayersService playersService)
    {
        _playersService = playersService;
    }

    public async Task<IActionResult> All()
    {
        var allPlayers = await this._playersService.GetAllAsync();

        return View(allPlayers);
    }

    public async Task<IActionResult> Collection()
    {
        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var myPlayers = await this._playersService.GetMyPlayersAsync(userId);

        return View(myPlayers);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(PlayerAddViewModel playerAddViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        await this._playersService.AddAsync(playerAddViewModel, userId);

        return RedirectToAction("All", "Players");
    }

    public async Task<IActionResult> AddToCollection(int id)
    {
        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        await this._playersService.AddToCollectionAsync(id, userId);

        return RedirectToAction("All", "Players");
    }

    public async Task<IActionResult> RemoveFromCollection(int id)
    {
        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        await this._playersService.RemoveFromCollectionAsync(id, userId);

        return RedirectToAction("Collection", "Players");
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var playerEditViewModel = await this._playersService.GetPlayerForEditAsync(id);

            return View(playerEditViewModel);
        }
        catch (Exception)
        {
            return RedirectToAction("All", "Players");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PlayerEditViewModel playerEditViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(playerEditViewModel);
        }

        try
        {
            await this._playersService.EditAsync(playerEditViewModel);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Something went wrong.");

            return View(playerEditViewModel);
        }
        

        return RedirectToAction("All", "Players");
    }
}
