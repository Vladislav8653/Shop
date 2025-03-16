using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.DTO;

namespace UserManagement.Application.UseCases.Commands.UserCommands.Register;

public record RegisterUserCommand : IRequest<IdentityResult>
{
    public RegisterUserDto RegisterUserDto { get; init; } = null!;
}