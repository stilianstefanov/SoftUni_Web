namespace BusStationApp.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

public class BusStationDbContext : IdentityDbContext
{
    public BusStationDbContext(DbContextOptions<BusStationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Destination> Destinations { get; set; } = null!;

    public DbSet<Ticket> Tickets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Ticket>()
            .Property(t => t.Price)
            .HasColumnType("decimal(18,2)");

        base.OnModelCreating(builder);
    }
}
