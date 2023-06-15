namespace BusStationApp.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Common.ValidationConstants.DestinationValidations;

public class Destination
{
    public Destination()
    {
        Tickets = new HashSet<Ticket>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(DestinationNameMaxLength)]
    public string DestinationName { get; set; } = null!;

    [Required]
    [MaxLength(OriginMaxLength)]
    public string Origin { get; set; } = null!;

    [Required]
    [MaxLength(DateMaxLength)]
    public string Date { get; set; } = null!;

    [Required]
    [MaxLength(TimeMaxLength)]
    public string Time { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = null!;
}
