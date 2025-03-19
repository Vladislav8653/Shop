using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Contracts.ConfirmTokenContracts;
using UserManagement.Application.Contracts.SmtpContracts;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Commands.ResetUserCommands.SendResetEmail;

public class SendResetEmailCommandHandler(
    UserManager<User> userManager,
    ISmtpService smtpService,
    IConfirmationTokenService confirmationTokenService)
    : IRequestHandler<SendResetEmailCommand, string>
{
    public async Task<string> Handle(SendResetEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.NewPasswordDto.Email);
        if (user == null)
        {
            throw new InvalidOperationException($"User with email {request.NewPasswordDto.Email} not found");
        }
        
        const string subject = "Reset Password";
        var body = $"Your reset token: {confirmationTokenService.CreateConfirmToken(user.Id)}";
        await smtpService.SendEmailAsync(
            user.UserName!, user.Email!, subject, body);
        return "Reset token sent successfully.";
    }
}