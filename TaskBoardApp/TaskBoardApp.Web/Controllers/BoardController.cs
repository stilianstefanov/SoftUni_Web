namespace TaskBoardApp.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Contracts;

[Authorize]
public class BoardController : Controller
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    public async Task<IActionResult> All()
    {
        var allBoards = await _boardService.GetAllAsync();

        return View(allBoards);
    }
}
