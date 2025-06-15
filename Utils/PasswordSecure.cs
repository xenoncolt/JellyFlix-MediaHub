using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Utils
{
    public static class PasswordSecure
    {
        private const int salt_size = 32;  // 256 bits storing
        private const int hash_size = 32;
        private const int iterations = 10000;  // NIST recommended minimum

        // cryptographically secure random salt
        public static string GenerateSaltPass()
        {
            byte[] salt = new byte[salt_size];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        // Hashed password with the provide salt using PBKDF2
        // PBKDF2 is a secure key derivation function recommended for password hashing
        public static string HashPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("Password and salt cant be empty");
            }

            byte[] salt_bytes = Convert.FromBase64String(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt_bytes, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(hash_size);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string salt, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(hash)) return false;

            string computed_hash = HashPassword(password, salt);
            return SecureCompare(computed_hash, hash);
        }

        private static bool SecureCompare(string x, string y)
        {
            if (x.Length != y.Length) return false;

            int result = 0;
            for (int i = 0; i <x.Length; i++)
            {
                // XOR each character - if they're the same, result will be 0
                // Using bitwise OR to accumulate any differences
                result |= x[i] ^ y[i];
            }
            return result == 0;
        } 
    }

}
