namespace UserManagement.Domain.CustomExceptions;

public class EmailNotConfirmedException(string message) : Exception(message);