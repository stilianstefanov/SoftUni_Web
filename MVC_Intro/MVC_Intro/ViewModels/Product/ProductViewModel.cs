namespace MVC_Intro.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;

    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [MinLength(2)]
        [MaxLength(10)]
        public string Name { get; set; } = null!;

        public double Price { get; set; }
    }
}
