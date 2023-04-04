﻿using System.Security.Cryptography;
using System.Text;

namespace Travel_Agency_API.Utils {
    public class Encryptor {
        private readonly static string key = "a32axh431h3u2137xddd6aa2137x1939";

        public static string EncryptString(string input) {
            byte[] iv = new byte[16];
            byte[] array;

            using (var aes = Aes.Create()) {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using var memoryStream = new MemoryStream();
                using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                using (var streamWriter = new StreamWriter(cryptoStream)) {
                    streamWriter.Write(input);
                }

                array = memoryStream.ToArray();
            }
            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string input) {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(input);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream(buffer);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }
    }
}