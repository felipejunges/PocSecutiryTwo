using Microsoft.Extensions.Configuration.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PocSecurityTwo.Security
{
    public class JsonCriptoConfigurationProvider : JsonConfigurationProvider
    {
        public ICryptoTransform Decryptor;
        private ICryptoTransform Encryptor;

        public JsonCriptoConfigurationProvider(JsonConfigurationSource source, byte[] key, byte[] iv) : base(source)
        {
            Aes aes = Aes.Create();
            Decryptor = aes.CreateDecryptor(key, iv);
            Encryptor = aes.CreateEncryptor(key, iv);
        }

        public override void Set(string key, string value)
        {
            byte[] textBytes = Encoding.Unicode.GetBytes(value);
            byte[] decryptedBytes = Encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
            base.Set(key, Convert.ToBase64String(decryptedBytes));
        }

        public override bool TryGet(string key, out string value)
        {
            if (base.TryGet(key, out value))
            {
                byte[] decryptedBytes = Convert.FromBase64String(value);
                byte[] textBytes = Decryptor.TransformFinalBlock(decryptedBytes, 0, decryptedBytes.Length);
                value = Encoding.Unicode.GetString(textBytes);
                return true;
            }
            return false;
        }
    }
}