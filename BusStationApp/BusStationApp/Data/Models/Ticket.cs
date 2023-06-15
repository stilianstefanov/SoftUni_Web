namespace BusStationApp.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Ticket
{
    [Key]
    public int Id { get; set; }

    public decimal Price { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(Destination))]
    public int DestinationId { get; set; }

    public Destination Destination { get; set; } = null!;
}
