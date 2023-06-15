namespace TaskBoardApp.Services;

using Microsoft.EntityFrameworkCore;
using Contracts;
using TaskBoardApp.Web.ViewModels.Task;
using Data;

public class TaskService : ITaskService
{
    private readonly TaskBoardDbContext _dbContext;

    public TaskService(TaskBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(TaskFormModel model, string userId)
    {
        var task = new Data.Models.Task()
        {
            Description = model.Description,
            Title = model.Title,
            BoardId = model.BoardId,
            CreatedOn = DateTime.UtcNow,
            OwnerId = userId
        };

        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TaskDetailsViewModel> GetTaskDetailsAsync(string id)
    {
        var viewModel = await _dbContext
            .Tasks
            .Select(t => new TaskDetailsViewModel()
            {
                Board = t.Board.Name,
                CreatedOn = t.CreatedOn.ToString("f"),
                Description = t.Description,
                Id = t.Id.ToString(),
                Owner = t.Owner.UserName,
                Title = t.Title
            })
            .FirstAsync(t => t.Id == id);


        return viewModel;
    }

    public async Task<TaskFormModel> GetTaskForEditAsync(string id)
    {
        var taskModel = await _dbContext
            .Tasks
            .FirstAsync(t => t.Id.ToString() == id);

        var viewModel = new TaskFormModel()
        {
            Title = taskModel.Title,
            Description = taskModel.Description,
            BoardId = taskModel.BoardId
        };

        return viewModel;
    }

    public async Task<TaskViewModel> GetTaskForDeleteAsync(string id)
    {
        var taskModel = await _dbContext
            .Tasks
            .FirstAsync(t => t.Id.ToString() == id);

        var viewModel = new TaskViewModel()
        {
            Id = taskModel.Id.ToString(),
            Title = taskModel.Title,
            Description = taskModel.Description
        };

        return viewModel;
    }

    public async Task EditAsync(string id, TaskFormModel model)
    {
        var taskModel = await _dbContext
            .Tasks
            .FirstAsync(t => t.Id.ToString() == id);

        taskModel.Title = model.Title;
        taskModel.Description = model.Description;
        taskModel.BoardId = model.BoardId;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var taskModel = await _dbContext
            .Tasks
            .FirstAsync(t => t.Id.ToString() == id);

        _dbContext.Tasks.Remove(taskModel);

        await _dbContext.SaveChangesAsync();
    }
}
