namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Library.Common.ValidationConstants.CategoryValidations;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; } = null!;
    }
}
