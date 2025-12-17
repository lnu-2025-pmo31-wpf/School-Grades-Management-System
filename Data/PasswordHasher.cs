using System;
using System.Security.Cryptography;

namespace SchoolJournal.Data
{
    internal static class PasswordHasher
    {
        // Для навчального проєкту цього більш ніж достатньо.
        private const int Iterations = 100_000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public static (byte[] hash, byte[] salt) HashPassword(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            return (hash, salt);
        }

        public static bool VerifyPassword(string password, byte[] expectedHash, byte[] salt)
        {
            if (password == null) return false;
            if (expectedHash == null || expectedHash.Length == 0) return false;
            if (salt == null || salt.Length == 0) return false;

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] actual = pbkdf2.GetBytes(HashSize);

            return CryptographicOperations.FixedTimeEquals(actual, expectedHash);
        }
    }
}
