using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using MediatR;  
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Contracts.SmtpContracts;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Commands.UserCommands.SendConfirmation;

public class SendConfirmationCommandHandler(
    ISmtpService smtpService,
    UserManager<User> userManager) : 
    IRequestHandler<SendConfirmationCommand, string>
{
    public async Task<string> Handle(SendConfirmationCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out var userIdGuid))
        {
            throw new ValidationException("UserId is invalid");
        }

        var user = await userManager.FindByIdAsync(userIdGuid.ToString());
        if (user == null)
        {
            throw new InvalidOperationException($"User with id {request.UserId} not found");
        }
        
        var hashUserId = SHA256.HashData(Encoding.UTF8.GetBytes(request.UserId));
        
        var emailBody = $"Your confirmation code: {hashUserId}";

        await smtpService.SendEmailAsync(user.UserName!, user.Email!, "Confirm your email", emailBody);
        return "Your confirmation code was sent on email.";
    }
}