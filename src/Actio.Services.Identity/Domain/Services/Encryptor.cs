using System;
using System.Security.Cryptography;
using System.Text;

namespace Actio.Services.Identity.Domain.Services
{
    public class Encryptor : IEncryptor
    {
        private static int salt_length_limit = 64;
        private static int pseudo_random_number = 64;
        private static int iterations_no = 10000;
        public (string, string) GetHash(string plainPassword)
        {
            var saltString = GetSaltBytes();
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(plainPassword, saltString, iterations_no))
            {
                return (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(pseudo_random_number)), 
                    Convert.ToBase64String(saltString));
            }
        }

        public string GetHash(string plainPassword, string salt)
        {
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(plainPassword, Encoding.UTF8.GetBytes(salt), iterations_no))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(pseudo_random_number));
            }
        }

        public byte[] GetSaltBytes()
        {
            // Empty salt array
            byte[] salt = new byte[salt_length_limit];
            using(var random = new RNGCryptoServiceProvider())
            {
                // Build the random bytes
                random.GetNonZeroBytes(salt);
            }
            // Return the bytes array of the generated salt
            return salt;
        }

        public string GetSalt()
        {
            // Empty salt array
            byte[] salt = new byte[salt_length_limit];
            using(var random = new RNGCryptoServiceProvider())
            {
                // Build the random bytes
                random.GetNonZeroBytes(salt);
            }
            // Return the string encoded salt
            return Convert.ToBase64String(salt);
        }
    }
}