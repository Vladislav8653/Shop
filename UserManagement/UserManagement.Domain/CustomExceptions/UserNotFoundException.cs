namespace UserManagement.Domain.CustomExceptions;

public class UserNotFoundException(string message) : Exception(message);