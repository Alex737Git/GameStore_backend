using Microsoft.AspNetCore.Identity;

namespace Entities.Models;

public class User:IdentityUser
{
    public string? FirstName { get; set; } 
    public string? LastName { get; set; }
    public string? AvatarUrl { get; set; }
    public ICollection<Game>? Games { get; set; }
    
}