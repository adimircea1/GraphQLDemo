using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace GraphQLDemo.Service.Extensions;

public static class UserPasswordExtension
{
    private const int SaltSize = 32;
    
    public static (string, string) HashPassword(this string password, string? salt = null)
    {
        salt ??= GenerateSalt();
        var saltedPassword = string.Concat(password, salt);
        var saltedPasswordToBytes = Encoding.UTF8.GetBytes(saltedPassword);
        var saltedPasswordBytesHashed = SHA256.HashData(saltedPasswordToBytes);
        var hashedPassword = Convert.ToBase64String(saltedPasswordBytesHashed);
        return (hashedPassword, salt);
    }

    public static bool VerifyPassword(this string inputPassword, string userPassword, string salt)
    {
        var hashedPassword = HashPassword(inputPassword, salt).Item1;
        return string.Equals(hashedPassword, userPassword);
    }

    private static string GenerateSalt()
    {
        using var random = RandomNumberGenerator.Create();
        var saltBytes = new byte[SaltSize];
        random.GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }
}