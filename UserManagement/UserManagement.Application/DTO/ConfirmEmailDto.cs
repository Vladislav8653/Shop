namespace UserManagement.Application.DTO;

public record ConfirmEmailDto
{
    public string ConfirmationCode { get; init; } 
}