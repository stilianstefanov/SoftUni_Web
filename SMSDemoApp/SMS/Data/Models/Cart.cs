namespace SMS.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class Cart
{
    public Cart()
    {
        Products = new HashSet<Product>();
    }

    [Key]
    public string Id { get; set; }

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;

    public virtual IdentityUser User { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = null!;
}
