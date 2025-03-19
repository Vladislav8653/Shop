using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Contracts.ConfirmTokenContracts;
using UserManagement.Domain.CustomExceptions;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Commands.ConfirmUserCommands.ConfirmEmail;

public class ConfirmEmailCommandHandler
    (UserManager<User> userManager, IConfirmationTokenService confirmationTokenService)
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

        if (!confirmationTokenService.ValidateToken(request.ConfirmationCode, request.UserId))
        {
            throw new EmailNotConfirmedException("Confirmation code is invalid");
        }
        user.EmailConfirmed = true;
        
        await userManager.UpdateAsync(user);
        
        return "Confirmed.";
    }
}