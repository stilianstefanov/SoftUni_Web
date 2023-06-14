namespace Library.ViewModels;

using System.ComponentModel.DataAnnotations;
using static Common.ValidationConstants.BookEntityValidations;

public class BookAddFormModel
{
    [Required]
    [StringLength(BookTitleMaxLength, MinimumLength = BookTitleMinLength,
        ErrorMessage = "Book Title must be between 10 and 50 symbols")]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(BookAuthorMaxLength, MinimumLength = BookAuthorMinLength,
        ErrorMessage = "Book Author must be between 5 and 50 symbols")]
    public string Author { get; set; } = null!;

    [Required]
    [StringLength(BookDescriptionMaxLength, MinimumLength = BookDescriptionMinLength,
        ErrorMessage = "Book Description must be between 5 and 5000 symbols")]
    public string Description { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public string Rating { get; set; } = null!;

    public int CategoryId { get; set; }

    public IEnumerable<CategorySelectViewModel>? Categories { get; set; }
}
