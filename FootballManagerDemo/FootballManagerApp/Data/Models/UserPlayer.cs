namespace FootballManagerApp.Data.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class UserPlayer
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;

    public virtual IdentityUser User { get; set; } = null!;

    [ForeignKey(nameof(Player))]
    public int PlayerId { get; set; }

    public virtual Player Player { get; set; } = null!;
}
