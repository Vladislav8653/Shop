using MediatR;

namespace UserManagement.Application.UseCases.Commands.UserCommands.SendConfirmation;

public record SendConfirmationCommand : IRequest<string>
{
    public string? UserId { get; init; }
}