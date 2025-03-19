namespace UserManagement.Application.DTO;

public record ResetPasswordDto
{
    public string Email { get; init; }
    public string NewPassword { get; init; }
    public string ResetToken { get; init; }
}