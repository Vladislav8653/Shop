using UserManagement.Application.DTO;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Contracts.AuthenticationContracts;

public interface IAuthenticationManager
{
    Task<User> ValidateUser(AuthenticateUserDto userForAuth);
    
    Task<TokenDto> CreateTokens(User user, bool populateExp);
    
    public Task<string> CreateAccessToken(User user);
}