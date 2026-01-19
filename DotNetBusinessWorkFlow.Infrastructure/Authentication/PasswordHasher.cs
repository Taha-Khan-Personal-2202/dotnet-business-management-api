using System.Security.Cryptography;
using System.Text;

namespace DotNetBusinessWorkFlow.Infrastructure.Authentication;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hmac = sha.ComputeHash(bytes);
        return Convert.ToBase64String(bytes);
    }

    public static bool Verfiy(string password, string hash)
    {
        var hashed = Hash(password);
        return hashed == hash;
    }
}
