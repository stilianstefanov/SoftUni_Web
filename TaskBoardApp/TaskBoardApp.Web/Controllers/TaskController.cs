namespace TaskBoardApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;

    using Services.Contracts;
    using TaskBoardApp.Web.ViewModels.Task;


    [Authorize]
    public class TaskController : Controller
    {
        private readonly IBoardService _boardService;
        private readonly ITaskService _taskService;

        public TaskController(IBoardService boardService, ITaskService taskService)
        {
            _boardService = boardService;
            _taskService = taskService;
        }

        public async Task<IActionResult> Create()
        {
            TaskFormModel model = new TaskFormModel();

            model.AllBoards = await _boardService.GetAllBoardsForSelectAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AllBoards = await _boardService.GetAllBoardsForSelectAsync();

                return View(viewModel);
            }

            bool isBoardValid = await _boardService.BoardExistByIdAsync(viewModel.BoardId);

            if (!isBoardValid)
            {
                ModelState.AddModelError(nameof(viewModel.BoardId), "Board does not exist!");
            }

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _taskService.CreateAsync(viewModel, userId);

            return RedirectToAction("All", "Board");
        }

        public async Task<IActionResult> Details(string id)
        {
            TaskDetailsViewModel viewModel = await _taskService.GetTaskDetailsAsync(id);

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            TaskFormModel viewModel = await _taskService.GetTaskForEditAsync(id);

            viewModel.AllBoards = await _boardService.GetAllBoardsForSelectAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TaskFormModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AllBoards = await _boardService.GetAllBoardsForSelectAsync();

                return View(viewModel);
            }

            bool isBoardValid = await _boardService.BoardExistByIdAsync(viewModel.BoardId);

            if (!isBoardValid)
            {
                ModelState.AddModelError(nameof(viewModel.BoardId), "Board does not exist!");
            }

            await _taskService.EditAsync(id, viewModel);

            return RedirectToAction("All", "Board");
        }

        public async Task<IActionResult> Delete(string id)
        {
            TaskViewModel viewModel = await _taskService.GetTaskForDeleteAsync(id);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel viewModel)
        {
            await _taskService.DeleteAsync(viewModel.Id);

            return RedirectToAction("All", "Board");
        }
    }
}
