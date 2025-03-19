using MediatR;

namespace UserManagement.Application.UseCases.Queries.UserQueries.SendConfirmation;

public record SendConfirmationCommand : IRequest<string>
{
    public string? UserId { get; init; }
}