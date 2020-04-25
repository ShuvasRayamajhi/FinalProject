using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace StegApp
{
    public static class Cryptography
    {
        private const int keySize = 256;
        private const int iterationOfDerivation = 1000;

        public static string Encrypt(string inputTxt, string password) //user entered text and password
        {
            byte[] saltStringBytes = Generate256BitsOfRandomEntropy();
            byte [] ivStringBytes = Generate256BitsOfRandomEntropy();
            byte[] plainBytes = Encoding.UTF8.GetBytes(inputTxt);
            using (Rfc2898DeriveBytes decryptKey = new Rfc2898DeriveBytes(password, saltStringBytes, iterationOfDerivation)) //generate the  decryption key using the inupt password
            {
                byte[] keyBytes = decryptKey.GetBytes(keySize / 8); //set the decryption key size
                using (RijndaelManaged algorithmAES = new RijndaelManaged()) //create RijndaelManaged object (used to encrypt text)
                {
                    algorithmAES.BlockSize = 256; //set block size
                    algorithmAES.Mode = CipherMode.CBC; //set cipher mode
                    algorithmAES.Padding = PaddingMode.PKCS7; //set padding
                    using (ICryptoTransform encryptor = algorithmAES.CreateEncryptor(keyBytes, ivStringBytes)) //create the encryptor for the stream
                    using (MemoryStream memoryStream = new MemoryStream()) //create memory stream for encryption
                    using (CryptoStream memStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        memStream.Write(plainBytes, 0, plainBytes.Length); //write
                        memStream.FlushFinalBlock();
                        byte[] cipherBytes = saltStringBytes;
                        cipherBytes = cipherBytes.Concat(ivStringBytes).ToArray();
                        cipherBytes = cipherBytes.Concat(memoryStream.ToArray()).ToArray();
                        memoryStream.Close();
                        memStream.Close();
                        string encryptedText = Convert.ToBase64String(cipherBytes); //generate cypher text and return
                        return encryptedText; //return the encrypted text
                    }
                }
            }
        }

        public static string Decrypt(string encryptedText, string password) //encrypted text, password
        {
            byte[] cipherTextBytesWithSaltAndIv = Convert.FromBase64String(encryptedText);
            byte[] saltStringBytes = cipherTextBytesWithSaltAndIv.Take(keySize / 8).ToArray();
            byte[] ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(keySize / 8).Take(keySize / 8).ToArray();
            byte[] cipherBytes = cipherTextBytesWithSaltAndIv.Skip((keySize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((keySize / 8) * 2)).ToArray();
            
            using (Rfc2898DeriveBytes decryptKey = new Rfc2898DeriveBytes(password, saltStringBytes, iterationOfDerivation)) //generate key 
            {
                byte[] keyBytes = decryptKey.GetBytes(keySize / 8); //specify key
                using (RijndaelManaged algorithmAES = new RijndaelManaged())  //create rijandaelmanged object
                {
                    algorithmAES.BlockSize = 256; //set block size
                    algorithmAES.Mode = CipherMode.CBC; //set cipher mode
                    algorithmAES.Padding = PaddingMode.PKCS7; //set padding
                    using (ICryptoTransform decryptor = algorithmAES.CreateDecryptor(keyBytes, ivStringBytes)) //create decryptor
                    using (MemoryStream memoryStream = new MemoryStream(cipherBytes))
                    using (CryptoStream memStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainBytes = new byte[cipherBytes.Length];
                        int decryptedByteCount = memStream.Read(plainBytes, 0, plainBytes.Length);
                        memoryStream.Close();
                        memStream.Close();
                        string decryptedText = Encoding.UTF8.GetString(plainBytes, 0, decryptedByteCount); //read the decrypted btyes and return value as string
                        return decryptedText;

                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            byte[] randomBytes = new byte[32]; 
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        public static string EncryptPassword(string decryptKey)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();//encrypt the decryptKey
            encrypt = md5.ComputeHash(encode.GetBytes(decryptKey));
            StringBuilder encryptdata = new StringBuilder(); //Create string 
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

    }
}