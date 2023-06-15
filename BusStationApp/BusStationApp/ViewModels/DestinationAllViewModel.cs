namespace BusStationApp.ViewModels;

public class DestinationAllViewModel
{
    public int Id { get; set; }

    public string DestinationName { get; set; } = null!;

    public string Origin { get; set; } = null!;

    public string DateAndTime { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public int TicketsCount { get; set; }
}
