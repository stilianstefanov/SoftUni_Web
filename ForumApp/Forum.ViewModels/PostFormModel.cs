namespace Forum.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.Validations.EntityValidations.Post;

    public class PostFormModel
    {

        [Required]
        [StringLength(TitleMaxLength), MinLength(TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLength), MinLength(ContentMinLength)]
        public string Content { get; set; } = null!;
    }
}
