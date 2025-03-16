using UserManagement.Application.DTO;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Contracts;

public interface IAuthenticationManager
{
    Task<bool> ValidateUser(AuthenticateUserDto userForAuth);
    
    Task<TokenDto> CreateTokens(User user, bool populateExp);
    
    public Task<string> CreateAccessToken(User user);
}