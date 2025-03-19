using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Contracts.ConfirmTokenContracts;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Commands.ResetUserCommands.ResetPassword;

public class ResetPasswordCommandHandler(
    UserManager<User> userManager,
    IConfirmationTokenService confirmationTokenService)
    : IRequestHandler<ResetPasswordCommand, string>
{
    public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.ResetPasswordDto.Email);
        if (user == null)
        {
            throw new InvalidOperationException($"User with email {request.ResetPasswordDto.Email} not found");
        }

        if (!confirmationTokenService.ValidateToken(request.ResetPasswordDto.Token, user.Id))
        {
            throw new InvalidOperationException("Invalid token.");
        }

        user.PasswordHash = request.ResetPasswordDto.NewPassword;
        await userManager.UpdateAsync(user);
        
        return "Password reset successfully.";
    }
}