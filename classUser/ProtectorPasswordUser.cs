using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace classUser
{
    public static class ProtectorPasswordUser
    {
        private static readonly byte[] salt =
            Encoding.Unicode.GetBytes("7BANANAS");
        private static readonly int iterations = 2000;
        public static int PasswordUsersFile = 1111;
        public static string Encrypt(string plainText, string password)
        {
            byte[] encryptedBytes;
            byte[] plainbytes = Encoding.Unicode.GetBytes(plainText);
            var aes = Aes.Create();

            var pdkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = pdkdf2.GetBytes(32);
            aes.IV = pdkdf2.GetBytes(16);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainbytes, 0, plainbytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);        
        }
        public static string Decrypt(string cryptoText, string password)
        {
            byte[] plainBytes;
            byte[] cryptoBytes = Convert.FromBase64String(cryptoText);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);
            using(var ms = new MemoryStream())
            {
                using(var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                }
                plainBytes = ms.ToArray();
            }
            return Encoding.Unicode.GetString(plainBytes);
        }
    }
}
