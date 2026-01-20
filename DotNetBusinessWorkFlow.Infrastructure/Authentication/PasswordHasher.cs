using System.Security.Cryptography;
using System.Text;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;

namespace DotNetBusinessWorkFlow.Infrastructure.Authentication;

public sealed class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }

    public bool Verify(string password, string passwordHash)
    {
        var computedHash = Hash(password);
        return computedHash == passwordHash;
    }
}
