namespace FootballManagerApp.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

public class FootballManagerDbContext : IdentityDbContext
{
    public FootballManagerDbContext(DbContextOptions<FootballManagerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<UserPlayer> UsersPlayers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<UserPlayer>()
            .HasKey(up => new { up.UserId, up.PlayerId });

        base.OnModelCreating(builder);
    }
}
