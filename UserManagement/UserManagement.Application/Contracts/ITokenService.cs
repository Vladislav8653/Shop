using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
namespace UserManagement.Application.Contracts;

public interface ITokenService
{
    SigningCredentials GetSigningCredentials();
    Task<List<Claim>> GetClaims();
    JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
    string GenerateRefreshToken();
}