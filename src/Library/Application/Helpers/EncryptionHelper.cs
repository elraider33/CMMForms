using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace Library.Application.Helpers
{
    public static class EncryptionHelper
    {
        public static bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);

        public static string HashPassword(string password) {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var hashed = BCrypt.Net.BCrypt.HashPassword(password, salt, false, BCrypt.Net.HashType.SHA256);
            return hashed;
        }

        public static string Sha256Hash(string password)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] resultingHash = hash.ComputeHash(enc.GetBytes(password));

                foreach (Byte b in resultingHash)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
            }
            return stringBuilder.ToString();
        }
    }
}
