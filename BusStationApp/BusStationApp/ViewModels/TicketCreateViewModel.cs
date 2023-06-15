namespace BusStationApp.ViewModels;

using System.ComponentModel.DataAnnotations;
using static Common.ValidationConstants.TicketValidations;

public class TicketCreateViewModel
{
    public int DestinationId { get; set; }

    [Required]
    [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
    public string Price { get; set; } = null!;

    [Required]
    [Range(TicketsCountMinValue, TicketsCountMaxValue)]
    public int TicketsCount { get; set; }
}
