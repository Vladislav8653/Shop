using MediatR;
using UserManagement.Application.DTO.HideUserDto;

namespace UserManagement.Application.UseCases.Commands.HideUserCommands;

public record HideUserCommand : IRequest<Unit>
{
    public string Token { get; init; }
    public HideUserDto HideUserDto { get; init; }
}