namespace TaskBoardApp.Web.ViewModels.Task;

using System.ComponentModel.DataAnnotations;
using Board;
using static Common.ValidationConstants.TaskEntityValidations;

public class TaskFormModel
{
    [Required]
    [StringLength(TaskTitleMaxLength, MinimumLength = TaskTitleMinLength,
        ErrorMessage = "Title should be at least {2} characters long.")]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(TaskDescriptionMaxLength, MinimumLength = TaskDescriptionMinLength,
        ErrorMessage = "Description should be at least {2} characters long.")]
    public string Description { get; set; } = null!;

    [Display(Name = "Board")]
    public int BoardId { get; set; }

    public IEnumerable<BoardSelectViewModel>? AllBoards { get; set; }
}
