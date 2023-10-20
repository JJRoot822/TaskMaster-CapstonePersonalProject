using System.Security.Cryptography;

namespace TaskMaster.Security;

public static class SecurityUtil
{
    public static string HashPassword(string password)
    {
        byte[] salt;
        RandomNumberGenerator.Create().GetBytes(salt = new byte[64]);

        // Hash password
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 350000, HashAlgorithmName.SHA512);
        byte[] hash = pbkdf2.GetBytes(128);

        // Combine salt and hash
        byte[] hashBytes = new byte[192];
        Array.Copy(salt, 0, hashBytes, 0, 64);
        Array.Copy(hash, 0, hashBytes, 64, 128);

        // Convert to base64 string and return
        return Convert.ToBase64String(hashBytes);
    }

    public static bool DoPasswordsMatch(string password, string hashedPassword)
    {
        // Convert hashed password back to bytes
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        // Extract salt and hash
        byte[] salt = new byte[64];
        Array.Copy(hashBytes, 0, salt, 0, 64);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 350000, HashAlgorithmName.SHA512);
        byte[] hash = pbkdf2.GetBytes(128);

        // Compare hashes
        for (int i = 0; i < 128; i++)
        {
            if (hashBytes[i + 64] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
}
