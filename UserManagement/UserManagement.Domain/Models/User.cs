using Microsoft.AspNetCore.Identity;

namespace UserManagement.Domain.Models;

public class User : IdentityUser
{
    public string? Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireTime { get; set; }
}