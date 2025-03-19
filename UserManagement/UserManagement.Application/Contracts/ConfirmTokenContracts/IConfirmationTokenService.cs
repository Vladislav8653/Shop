namespace UserManagement.Application.Contracts.ConfirmTokenContracts;

public interface IConfirmationTokenService
{
    public string CreateConfirmToken(string content);
    public bool ValidateToken(string token, string originalContent);
}