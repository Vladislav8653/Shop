﻿using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.CustomExceptions;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Commands.ConfirmUserCommands.ConfirmEmail;

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

        var result = await userManager.ConfirmEmailAsync(user, request.ConfirmationCode);
        
        if (!result.Succeeded)
        {
            throw new EmailNotConfirmedException("Confirmation code is invalid");
        }
        
        return "Confirmed.";
    }
}