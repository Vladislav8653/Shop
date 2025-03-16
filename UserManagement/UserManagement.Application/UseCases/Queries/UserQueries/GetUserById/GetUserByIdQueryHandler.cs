using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.CustomExceptions;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Queries.UserQueries.GetUserById;

public class GetUserByIdQueryHandler(
    UserManager<User> userManager) 
    : IRequestHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        
        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }
        
        return user;
    }
}