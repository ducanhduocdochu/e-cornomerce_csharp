using System;
using System.Security.Cryptography;

namespace Backend.Utils
{
    public class KeyGenerator
    {
        public static void GenerateKeys(out string publicKey, out string privateKey)
        {
            using (RSA rsa = RSA.Create())
            {
                // You can specify the key size. 2048 is a commonly used size for RSA.
                rsa.KeySize = 2048;

                publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
                privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            }
        }
    }
}