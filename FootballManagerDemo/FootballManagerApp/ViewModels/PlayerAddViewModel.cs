namespace FootballManagerApp.ViewModels;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidations.PlayerValidations;

public class PlayerAddViewModel
{
    [Required]
    [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength, ErrorMessage = "FullName should be between 5 and 80 symbols")]
    public string FullName { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [MaxLength(PositionMaxLength)]
    [StringLength(PositionMaxLength, MinimumLength = PositionMinLength, ErrorMessage = "Position should be between 5 and 20 symbols")]
    public string Position { get; set; } = null!;

    [Required]
    [Range(SpeedMinValue, SpeedMaxValue, ErrorMessage = "Speed should be between 0 and 10")]
    public byte Speed { get; set; }

    [Required]
    [Range(SpeedMinValue, SpeedMaxValue, ErrorMessage = "Endurance should be between 0 and 10")]
    public byte Endurance { get; set; }

    [Required]
    [MaxLength(DescriptionMaxLength, ErrorMessage = "Description should be up to 200 symbols")]
    public string Description { get; set; } = null!;
}
