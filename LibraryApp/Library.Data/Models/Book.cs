namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Library.Common.ValidationConstants.BookEntityValidations;

    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BookTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(BookAuthorMaxLength)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(BookDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal Rating { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<IdentityUserBook> UsersBooks { get; set; } = null!;
    }
}
