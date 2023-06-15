namespace TaskBoardApp.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Models.Task;

internal class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasOne(t => t.Board)
            .WithMany(b => b.Tasks)
            .HasForeignKey(t => t.BoardId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(GenerateTasks());
    }

    private Task[] GenerateTasks()
    {
        ICollection<Task> tasks = new HashSet<Task>()
        {
            new Task()
            {
                Title = "Improve CSS styles",
                Description = "Implement better styling for all public pages",
                CreatedOn = DateTime.UtcNow.AddDays(-200),
                OwnerId = "5b7d619d-6f74-4f61-b386-f752958b8ea3",
                BoardId = 1
            },
            new Task()
            {
                Title = "Android Client App",
                Description = "Create Android client App for the RESTful TaskBoard service",
                CreatedOn = DateTime.UtcNow.AddMonths(-5),
                OwnerId = "5b7d619d-6f74-4f61-b386-f752958b8ea3",
                BoardId = 1
            },
            new Task()
            {
                Title = "Desktop Client App",
                Description = "Create Desktop client App for the RESTful TaskBoard service",
                CreatedOn = DateTime.UtcNow.AddMonths(-1),
                OwnerId = "f7e2d916-39c6-4240-8c80-185c2fea89b3",
                BoardId = 2
            },
            new Task()
            {
                Title = "Create Tasks",
                Description = "Implement [Create Task] page for adding tasks",
                CreatedOn = DateTime.UtcNow.AddYears(-1),
                OwnerId = "f7e2d916-39c6-4240-8c80-185c2fea89b3",
                BoardId = 3
            }
        };

        return tasks.ToArray();
    }
}
