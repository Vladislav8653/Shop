using System.Text.Json.Serialization;
using MediatR;

namespace UserManagement.Application.UseCases.Commands.UserCommands.ConfirmEmail;

public record ConfirmEmailCommand : IRequest<string>
{
    [JsonIgnore]
    public string? UserId { get; init; }
    public string ConfirmationCode { get; init; } = null!;
}