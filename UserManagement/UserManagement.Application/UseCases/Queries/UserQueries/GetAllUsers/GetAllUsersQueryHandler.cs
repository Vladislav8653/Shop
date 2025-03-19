﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Contracts.SmtpContracts;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Queries.UserQueries.GetAllUsers;

public class GetAllUsersQueryHandler(
    UserManager<User> userManager) 
    : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    public async Task<IEnumerable<User>> Handle(
        GetAllUsersQuery request, 
        CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
            
        return users;
    }
}