namespace Homies.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Common.ValidationConstants.TypeValidations;

public class Type
{
    public Type()
    {
        Events = new HashSet<Event>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = null!;
}
