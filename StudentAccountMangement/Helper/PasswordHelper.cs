using System.Security.Cryptography;
using System.Text;

namespace StudentAccountMangement.Helper
{
    public class PasswordHelper
    {
        private static readonly string encryptionKey = "idgfvwgbvsjhvjdsbvjhbkhbvkvkjwhbvuhd";

        public static string Encrypt(string plainText)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 32));
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.GenerateIV();
                byte[] iv = aes.IV;

                using var encryptor = aes.CreateEncryptor(aes.Key, iv);
                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var writer = new StreamWriter(cs))
                {
                    writer.Write(plainText);
                }

                byte[] encryptedContent = ms.ToArray();
                byte[] result = new byte[iv.Length + encryptedContent.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encryptedContent, 0, result, iv.Length, encryptedContent.Length);

                return Convert.ToBase64String(result);
            }
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] fullCipher = Convert.FromBase64String(encryptedText);
            byte[] iv = new byte[16];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 32));
            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(cipher);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);
            return reader.ReadToEnd();
        }
    }
}
