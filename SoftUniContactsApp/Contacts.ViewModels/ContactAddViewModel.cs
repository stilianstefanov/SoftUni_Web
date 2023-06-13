namespace Contacts.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ValidationConstants.ContactEntityValidations;

    public class ContactAddViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength, ErrorMessage = "First Name should be between 2 and 50 symbols")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength, ErrorMessage = "Last Name should be between 5 and 50 symbols")]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength, ErrorMessage = "Phone number should be between 10 and 13 symbols")]
        [RegularExpression(PhoneNumberRegex)]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        [Required]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength, ErrorMessage = "Email should be between 10 and 60 symbols")]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(WebsiteRegex)]
        public string Website { get; set; } = null!;

    }
}
