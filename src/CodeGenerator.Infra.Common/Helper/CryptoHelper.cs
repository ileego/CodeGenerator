using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CodeGenerator.Infra.Common.Helper
{
    public class CryptoHelper
    {

        #region 3DES加密解密

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="data">16进制串</param>
        /// <param name="key">16进制串</param>
        /// <returns>16进制串</returns>
        public static string Des3Encrypt(string data, string key)
        {
            var source = Encoding.UTF8.GetBytes(data);
            var keys = new byte[24];
            var keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Copy(keyBytes, 0, keys, 0, keyBytes.Length);
            var iv = Encoding.UTF8.GetBytes("0000000000000000");

            var r = EncryptDataToMemory(source, keys, iv);

            return Convert.ToBase64String(r);
        }

        public static byte[] Des3Encrypt(byte[] data, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros)
        {
            var r = EncryptDataToMemory(data, key, iv, cipherMode, paddingMode);

            return r;
        }

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        private static byte[] EncryptDataToMemory(byte[] data, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros)
        {
            var tdes = new TripleDESCryptoServiceProvider
            {
                Mode = cipherMode,//CipherMode.ECB,
                Padding = paddingMode//PaddingMode.Zeros
            };

            // Create a MemoryStream.
            var mStream = new MemoryStream();

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            var cStream = new CryptoStream(mStream,
                tdes.CreateEncryptor(key, iv),
                CryptoStreamMode.Write);

            // Write the byte array to the crypto stream and flush it.
            cStream.Write(data, 0, data.Length);
            cStream.FlushFinalBlock();

            // Get an array of bytes from the 
            // MemoryStream that holds the 
            // encrypted data.
            var ret = mStream.ToArray();

            // Close the streams.
            cStream.Close();
            mStream.Close();

            // Return the encrypted buffer.
            return ret;

        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="data">16进制串</param>
        /// <param name="key">16进制串</param>
        /// <returns></returns>
        public static string Des3Decrypt(string data, string key)
        {
            var source = Convert.FromBase64String(data);
            var keys = new byte[24];
            var keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Copy(keyBytes, 0, keys, 0, keyBytes.Length);
            var iv = Encoding.UTF8.GetBytes("0000000000000000");

            var r = DecryptDataFromMemory(source, keys, iv);

            return Encoding.UTF8.GetString(r).Replace("\0", "");
        }
        public static byte[] Des3Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            var r = DecryptDataFromMemory(data, key, iv);

            return r;
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        private static byte[] DecryptDataFromMemory(byte[] data, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros)
        {
            var tdes = new TripleDESCryptoServiceProvider
            {
                Mode = cipherMode,//CipherMode.ECB
                Padding = paddingMode//PaddingMode.Zeros
            };

            // Create a new MemoryStream using the passed 
            // array of encrypted data.
            var msDecrypt = new MemoryStream(data);

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            var csDecrypt = new CryptoStream(msDecrypt,
                tdes.CreateDecryptor(key, iv),
                CryptoStreamMode.Read);

            // Create buffer to hold the decrypted data.
            var fromEncrypt = new byte[data.Length];

            // Read the decrypted data out of the crypto stream
            // and place it into the temporary buffer.
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

            //Convert the buffer into a string and return it.
            return fromEncrypt;

        }

        #endregion
    }
}
