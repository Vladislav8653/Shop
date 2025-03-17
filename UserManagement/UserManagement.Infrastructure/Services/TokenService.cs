using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Application.Contracts;
using UserManagement.Domain.Models;

namespace UserManagement.Infrastructure.Services;

public class TokenService(
    UserManager<User> userManager,
    IConfiguration configuration) : ITokenService
{
    public SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        
        var secretKey = jwtSettings.GetSection("validIssuer").Value;
        
        var key = Encoding.UTF8.GetBytes(secretKey!);
        
        var secret = new SymmetricSecurityKey(key);
        
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    
    public async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.NameIdentifier, user.Id)
        };
        
        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        return claims;
    }
    
    public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings.GetSection("validIssuer").Value,
            //audience: jwtSettings.GetSection("validAudience").Value,
            claims: claims,
            expires:
            DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
            signingCredentials: signingCredentials
        );
        
        return tokenOptions;
    }
    
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        return Convert.ToBase64String(randomNumber);
    }
}