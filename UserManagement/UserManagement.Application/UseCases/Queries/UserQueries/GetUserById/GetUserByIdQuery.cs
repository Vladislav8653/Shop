using MediatR;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Queries.UserQueries.GetUserById;

public record GetUserByIdQuery : IRequest<User>
{
    public string UserId { get; init; } = null!;
}