namespace Contacts.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;


    public class ApplicationUserContact
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; } = null!;

        public virtual IdentityUser ApplicationUser { get; set; } = null!;

        [ForeignKey(nameof(Contact))]
        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; } = null!;
    }
}
