using MediatR;
using UserManagement.Application.DTO;
using UserManagement.Application.DTO.PasswordResetDto;

namespace UserManagement.Application.UseCases.Commands.ResetUserCommands.ResetPassword;

public record ResetPasswordCommand : IRequest<string>
{
    public ResetPasswordDto ResetPasswordDto { get; init; }
}