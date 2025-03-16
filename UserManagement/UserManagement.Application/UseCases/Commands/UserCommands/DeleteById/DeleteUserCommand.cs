using MediatR;

namespace UserManagement.Application.UseCases.Commands.UserCommands.DeleteById;

public record DeleteUserCommand : IRequest<Unit>
{ 
    public string UserId { get; init; } = null!;
}