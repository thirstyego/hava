using Microsoft.AspNetCore.Identity;

namespace hava.Models;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public ICollection<Home> Homes { get; set; }
}