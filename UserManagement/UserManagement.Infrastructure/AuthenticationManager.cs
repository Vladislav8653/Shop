using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UserManagement.Application.Contracts;
using UserManagement.Application.DTO;
using UserManagement.Domain.Models;
using UserManagement.Infrastructure.CustomExceptions;

namespace UserManagement.Infrastructure;

public class AuthenticationManager(
    UserManager<User> userManager,
    IConfiguration configuration,
    ITokenService tokenService) : IAuthenticationManager
{
    private User? _user;
    
    public async Task<bool> ValidateUser(AuthenticateUserDto userForAuth)
    {
        _user = await userManager.FindByNameAsync(userForAuth.UserName);
        if (_user is null)
            throw new UserNotFoundException("User not found");   
        return await userManager.CheckPasswordAsync(_user, userForAuth.Password);
    }

    public async Task<TokenDto> CreateTokens(User user, bool populateExp)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var refreshTokenLifeTimeStr = jwtSettings.GetSection("RefreshTokenLifeTime").Value;
        if (refreshTokenLifeTimeStr is null)
            throw new InvalidOperationException ("RefreshTokenLifeTime is null");
        var refreshTokenLifeTime = int.Parse(refreshTokenLifeTimeStr);
        
        _user = user;
        
        var signingCredentials = tokenService.GetSigningCredentials();
        var claims = await tokenService.GetClaims();
        var tokenOptions = tokenService.GenerateTokenOptions(signingCredentials, claims);
        
        var refreshToken = tokenService.GenerateRefreshToken();
        _user.RefreshToken = refreshToken;
        
        if (populateExp)
            _user.RefreshTokenExpireTime = DateTime.UtcNow.AddMinutes(refreshTokenLifeTime);
        await userManager.UpdateAsync(_user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        
        return new TokenDto(accessToken, refreshToken);
    }

    public async Task<string> CreateAccessToken(User user)
    {
        _user = user;
        
        var signingCredentials = tokenService.GetSigningCredentials();
        var claims = await tokenService.GetClaims();
        var tokenOptions = tokenService.GenerateTokenOptions(signingCredentials, claims);
        
        await userManager.UpdateAsync(_user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        
        return accessToken;
    }
    
    public async Task Logout(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundException("User not found.");

        user.RefreshToken = null;
        await userManager.UpdateAsync(user);
    }
    
}