using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PW_Manager.Scripts
{
    public class Crypto
    {
        public string EncryptData(string _input, string _key)
        {
            string pw = _key;
            byte[] Key = Encoding.UTF8.GetBytes(pw);

            AesManaged aes = new AesManaged();
            aes.Key = Key;
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            MemoryStream ms = new MemoryStream();
            CryptoStream crypto = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] inputData = Encoding.UTF8.GetBytes(_input);
            crypto.Write(inputData, 0, inputData.Length);
            crypto.FlushFinalBlock();

            byte[] outputData = ms.ToArray();
            return Convert.ToBase64String(outputData);
        }

        public string DecryptData(string _input, string _key)
        {
            try {
                string pw = _key;
                byte[] Key = Encoding.UTF8.GetBytes(pw);

                AesManaged aes = new AesManaged();
                aes.Key = Key;
                aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                MemoryStream ms = new MemoryStream();
                CryptoStream crypto = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);

                byte[] inputData = Convert.FromBase64String(_input);
                crypto.Write(inputData, 0, inputData.Length);
                crypto.FlushFinalBlock();

                byte[] outputData = ms.ToArray();

                return UTF8Encoding.UTF8.GetString(outputData, 0, outputData.Length);
            } catch {
                return "unreadable";
            }
            
        }
    }
}