namespace Forum.Data;

using Microsoft.EntityFrameworkCore;
using Models;
using Configuration;

public class ForumDbContext : DbContext
{
    public ForumDbContext(DbContextOptions<ForumDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
