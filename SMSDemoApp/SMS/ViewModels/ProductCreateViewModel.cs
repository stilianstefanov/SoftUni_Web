namespace SMS.ViewModels;

using System.ComponentModel.DataAnnotations;
using static Common.ValidationConstants.ProductValidations;

public class ProductCreateViewModel
{
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
    public string Price { get; set; } = null!;
}
