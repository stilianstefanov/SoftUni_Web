namespace TaskBoardApp.Data.Models;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.ValidationConstants.TaskEntityValidations;

public class Task
{
    public Task()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; private set; }

    [Required]
    [MaxLength(TaskTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(TaskDescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    [ForeignKey(nameof(Board))]
    public int BoardId { get; set; }

    public Board Board { get; set; } = null!;

    [ForeignKey(nameof(Owner))]
    public string OwnerId { get; set; } = null!;

    public IdentityUser Owner { get; set; } = null!;
}
