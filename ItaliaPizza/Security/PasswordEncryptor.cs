using System;
using System.Text;
using System.Security.Cryptography;

namespace Security
{
    public static class PasswordEncryptor
    {
        public static string ComputeSHA512Hash(string password)
        {
            var encryptedPassword = "";
            using (SHA512 sHA512Hash = SHA512.Create())
            {
                byte[] hashvalue = sHA512Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                encryptedPassword = BitConverter.ToString(hashvalue)
                .Replace("-", string.Empty)
                .ToLowerInvariant();
            }
            return encryptedPassword;
        }
    }
    
}
