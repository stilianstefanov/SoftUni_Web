namespace FootballManagerApp.ViewModels;

public class PlayerViewModel
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string Position { get; set; } = null!;

    public byte Speed { get; set; }

    public byte Endurance { get; set; }

    public string Description { get; set; } = null!;
}
