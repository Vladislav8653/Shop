namespace UserManagement.Application.DTO;

public record RegisterUserDto
{
    public enum UserRole
    {
        User = 1,
        Administrator = 2
    }

    public RegisterUserDto(string firstName, string lastName, string userName,
        string email, string password, UserRole role)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        Password = password;
        Role = role;
    }

    public string FirstName { get; init; } 
    public string LastName { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public UserRole Role { get; init; }
}