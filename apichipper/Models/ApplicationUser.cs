using Microsoft.AspNetCore.Identity;

namespace apichipper.Models;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public ICollection<Home> Homes { get; set; }
}