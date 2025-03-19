using MediatR;
using UserManagement.Application.DTO;

namespace UserManagement.Application.UseCases.Commands.ResetUserCommands.SendResetEmail;

public record SendResetEmailCommand : IRequest<string>
{ 
    public EmailForPasswordResetDto NewPasswordDto { get; init; }
}