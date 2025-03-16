﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.CustomExceptions;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Commands.UserCommands.DeleteById;

public class DeleteUserCommandHandler(UserManager<User> userManager)
    : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            throw new UserNotFoundException("User not found");
        }

        await userManager.DeleteAsync(user);

        return Unit.Value;
    }
}