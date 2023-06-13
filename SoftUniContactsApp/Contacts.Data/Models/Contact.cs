namespace Contacts.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ValidationConstants.ContactEntityValidations;

    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        public string Website { get; set; } = null!;

        public virtual ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; } = null!;
    }
}
