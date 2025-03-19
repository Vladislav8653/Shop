namespace UserManagement.Application.DTO;

public record EmailForPasswordResetDto
{
    public string Email { get; init; }
}