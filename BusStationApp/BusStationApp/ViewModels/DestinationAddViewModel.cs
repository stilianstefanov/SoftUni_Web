namespace BusStationApp.ViewModels;

using System.ComponentModel.DataAnnotations;
using static Common.ValidationConstants.DestinationValidations;

public class DestinationAddViewModel
{

    [Required]
    [StringLength(DestinationNameMaxLength, MinimumLength = DestinationNameMinLength)]
    public string DestinationName { get; set; } = null!;

    [Required]
    [StringLength(OriginMaxLength, MinimumLength = OriginMinLength)]
    public string Origin { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string ImageUrl { get; set; } = null!;
}
