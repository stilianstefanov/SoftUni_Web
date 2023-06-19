namespace Homies.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static Common.ValidationConstants.EventValidations;

public class Event
{
    public Event()
    {
        EventsParticipants = new HashSet<EventParticipant>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [ForeignKey(nameof(Organiser))]
    public string OrganiserId { get; set; } = null!;

    public virtual IdentityUser Organiser { get; set; } = null!;

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime Start { get; set; }

    [Required]
    public DateTime End { get; set; }

    [ForeignKey(nameof(Type))]
    public int TypeId { get; set; }

    public virtual Type Type { get; set; } = null!;

    public virtual ICollection<EventParticipant> EventsParticipants { get; set; } = null!;
}
