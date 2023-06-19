namespace Homies.ViewModels.Event;

using System.ComponentModel.DataAnnotations;
using Type;
using static Common.ValidationConstants.EventValidations;

public class EventEditViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength,
        ErrorMessage = "Name should be between 5 and 20 symbols")]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength,
        ErrorMessage = "Description should be between 15 and 150 symbols")]
    public string Description { get; set; } = null!;

    [Required]
    public string Start { get; set; } = null!;

    [Required]
    public string End { get; set; } = null!;

    public int TypeId { get; set; }

    public IEnumerable<TypeDropDownViewModel>? Types { get; set; }
}
