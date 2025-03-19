using System.Security.Cryptography;
using System.Text;
using UserManagement.Application.Contracts.ConfirmTokenContracts;

namespace UserManagement.Application.ConfirmTokenService;

public class ConfirmationTokenService : IConfirmationTokenService
{
    public string CreateConfirmToken(string content)
    {
        var hashToken = SHA256.HashData(Encoding.UTF8.GetBytes(content));
        var hashString = Convert.ToBase64String(hashToken);
        return hashString;
    }

    public bool ValidateToken(string token, string originalContent)
    {
        return token == CreateConfirmToken(originalContent);
    }
}