namespace FootballManagerApp.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidations.PlayerValidations;

public class Player
{
    public Player()
    {
        UserPlayers = new HashSet<UserPlayer>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(FullNameMaxLength)]
    public string FullName { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [MaxLength(PositionMaxLength)]
    public string Position { get; set; } = null!;

    public byte Speed { get; set; }

    public byte Endurance { get; set; }

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public virtual ICollection<UserPlayer> UserPlayers { get; set; } = null!;
}
