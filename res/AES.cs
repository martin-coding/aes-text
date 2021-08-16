using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AESTEXT
{
    class AES
    {
        AesCryptoServiceProvider crypto_provider;

        static byte[] pw(string value)
        {
            using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
            {
                ASCIIEncoding ascii = new ASCIIEncoding();
                byte[] data = sha256.ComputeHash(ascii.GetBytes(value));
                return data;
            }
        }
        static byte[] iv(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                ASCIIEncoding ascii = new ASCIIEncoding();
                byte[] data = md5.ComputeHash(ascii.GetBytes(value));
                return data;
            }
        }

        public AES()
        {
            crypto_provider = new AesCryptoServiceProvider();

            crypto_provider.BlockSize = 128;
            crypto_provider.KeySize = 256;
            crypto_provider.Key = pw(MainWindow.pwinput);
            crypto_provider.IV = iv(MainWindow.ivinput);
            crypto_provider.Mode = CipherMode.CFB;
            crypto_provider.Padding = PaddingMode.PKCS7;
        }

        public String encrypt(String clear_text)
        {
            ICryptoTransform transform = crypto_provider.CreateEncryptor();

            byte[] encrypted_bytes = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(clear_text), 0, clear_text.Length);

            string str = Convert.ToBase64String(encrypted_bytes);
            return str;
        }

        public String decrypt(String cipher_text)
        {
            ICryptoTransform transform = crypto_provider.CreateDecryptor();

            byte[] enc_bytes = Convert.FromBase64String(cipher_text);
            byte[] decrypted_bytes = transform.TransformFinalBlock(enc_bytes, 0, enc_bytes.Length);

            string str = ASCIIEncoding.ASCII.GetString(decrypted_bytes);
            return str;
        }
    }
}
