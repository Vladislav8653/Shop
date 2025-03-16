namespace UserManagement.Infrastructure.CustomExceptions;

public class UserNotFoundException(string message) : Exception(message);