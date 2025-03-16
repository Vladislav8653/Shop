using MediatR;
using UserManagement.Domain.Models;

namespace UserManagement.Application.UseCases.Queries.UserQueries.GetAllUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<User>>;