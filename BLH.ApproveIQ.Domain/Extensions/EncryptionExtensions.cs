using System.Security.Cryptography;

namespace BLH.ApproveIQ.Domain.Extensions;

public sealed class EncryptionExtensions
{
    private const int _saltLengthLimit = 32;

    public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
    {
        HashAlgorithm algorithm = SHA256.Create();

        byte[] plainTextWithSaltBytes =
          new byte[plainText.Length + salt.Length];

        for (int i = 0; i < plainText.Length; i++)
        {
            plainTextWithSaltBytes[i] = plainText[i];
        }
        for (int i = 0; i < salt.Length; i++)
        {
            plainTextWithSaltBytes[plainText.Length + i] = salt[i];
        }

        return algorithm.ComputeHash(plainTextWithSaltBytes);
    }

    public static byte[] GetSalt()
    {
        var salt = new byte[_saltLengthLimit];
        using (var random = RandomNumberGenerator.Create())
        {
            random.GetNonZeroBytes(salt);
        }

        return salt;
    }
}
