namespace UserManagement.Application.DTO;

public record UserRequestDto
{
    public enum UserRole
    {
        User = 1,
        Administrator = 2
    }

    public UserRequestDto(string userName, string email, string password, UserRole role)
    {
        UserName = userName;
        Email = email;
        PasswordHash = password;
        Role = role;
    }
    
    public string UserName { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; init; }
    public UserRole Role { get; init; }
}