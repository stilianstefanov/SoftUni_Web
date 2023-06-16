namespace SMS.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.ValidationConstants.ProductValidations;

public class Product
{
    public Product()
    {
    }

    [Key]
    public string Id { get; set; } 

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    [ForeignKey(nameof(Cart))]
    public string? CartId { get; set; }

    public virtual Cart? Cart { get; set; }
}
