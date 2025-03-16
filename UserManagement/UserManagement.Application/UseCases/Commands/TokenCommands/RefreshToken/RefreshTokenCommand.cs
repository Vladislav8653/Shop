using MediatR;
using UserManagement.Application.DTO;

namespace UserManagement.Application.UseCases.Commands.TokenCommands.RefreshToken;

public record RefreshTokenCommand : IRequest<string>
{
    public TokenDto TokenDto { get; init; } = null!;
}