namespace TaskBoardApp.Services.Contracts;

using TaskBoardApp.Web.ViewModels.Task;

public interface ITaskService
{
    Task CreateAsync(TaskFormModel model, string userId);

    Task<TaskDetailsViewModel> GetTaskDetailsAsync(string id);

    Task<TaskFormModel> GetTaskForEditAsync(string id);

    Task<TaskViewModel> GetTaskForDeleteAsync(string id);

    Task EditAsync(string id, TaskFormModel model);

    Task DeleteAsync(string id);
}
