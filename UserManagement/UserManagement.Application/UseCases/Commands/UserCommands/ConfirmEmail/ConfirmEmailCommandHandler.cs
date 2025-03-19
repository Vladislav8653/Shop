using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.CustomExceptions;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Commands.UserCommands.ConfirmEmail;

public class ConfirmEmailCommandHandler
    (UserManager<User> userManager)
    : IRequestHandler<ConfirmEmailCommand, string>
{
    public async Task<string> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
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
        var hashString = Convert.ToBase64String(hashUserId);

        if (!string.Equals(hashString, request.ConfirmationCode))
        {
            throw new EmailNotConfirmedException("Confirmation code is invalid");
        }
        user.EmailConfirmed = true;
        await userManager.UpdateAsync(user);
        return "Confirmed.";
    }
}