using MediatR;
using UserManagement.Application.DTO;

namespace UserManagement.Application.UseCases.Commands.UserCommands.Authenticate;

public record AuthenticateUserCommand
    : IRequest<(string AccessToken, string RefreshToken)>
{
    public AuthenticateUserDto AuthenticateUserDto { get; init; } = null!;
}