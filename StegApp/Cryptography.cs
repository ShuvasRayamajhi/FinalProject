using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace StegApp
{
    public static class Cryptography
    {
        private const int iterationOfDerivation = 1000; //used for the password generation function
        private const int keySize = 256; //variable that decides the encryption key size

        private static byte[] Generate256BitsOfRandomEntropy() //generate random bytes for encryption 
        {
            byte[] randomBytes = new byte[32];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                rngCsp.GetBytes(randomBytes);
          
            return randomBytes;
        }

        public static string Encryption(string inputTxt, string password) //user entered text and password
        {
            byte[] ivBytes = Generate256BitsOfRandomEntropy(); //declare iv bytes variable
            byte[] plainBytes = Encoding.UTF8.GetBytes(inputTxt); //declare plain text byte variable
            byte[] saltBytes = Generate256BitsOfRandomEntropy(); //declare salt byte variable
            using (Rfc2898DeriveBytes decryptKey = new Rfc2898DeriveBytes(password, saltBytes, iterationOfDerivation)) //generate the  decryption key using the inupt password
            {
                byte[] keyBytes = decryptKey.GetBytes(keySize / 8); //set the decryption key size
                using (RijndaelManaged algorithmAES = new RijndaelManaged()) //create RijndaelManaged object (used to encrypt text)
                {
                    algorithmAES.Mode = CipherMode.CBC; //set cipher mode
                    algorithmAES.Padding = PaddingMode.PKCS7; //set padding
                    algorithmAES.BlockSize = 256; //set block size
                    using (ICryptoTransform encryptor = algorithmAES.CreateEncryptor(keyBytes, ivBytes)) //create the encryptor for the stream
                    using (MemoryStream memoryStream = new MemoryStream()) //create memory stream for encryption
                    using (CryptoStream memStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        memStream.Write(plainBytes, 0, plainBytes.Length); //write
                        memStream.FlushFinalBlock();
                        byte[] cipherBytes = saltBytes;
                        cipherBytes = cipherBytes.Concat(ivBytes).ToArray();
                        cipherBytes = cipherBytes.Concat(memoryStream.ToArray()).ToArray();
                        memoryStream.Close();
                        memStream.Close();
                        string encryptedText = Convert.ToBase64String(cipherBytes); //generate cypher text and return
                        return encryptedText; //return the encrypted text
                    }
                }
            }
        }
        public static string Decryption(string encryptedText, string password) //encrypted text, password
        {
            byte[] cipherBytesWithSaltIv = Convert.FromBase64String(encryptedText); //get the cipher text, that was extract from the image
            byte[] saltBytes = cipherBytesWithSaltIv.Take(keySize / 8).ToArray();
            byte[] ivBytes = cipherBytesWithSaltIv.Skip(keySize / 8).Take(keySize / 8).ToArray();
            byte[] cipherBytes = cipherBytesWithSaltIv.Skip((keySize / 8) * 2).Take(cipherBytesWithSaltIv.Length - ((keySize / 8) * 2)).ToArray();
            
            using (Rfc2898DeriveBytes decryptKey = new Rfc2898DeriveBytes(password, saltBytes, iterationOfDerivation)) //generate key 
            {
                byte[] keyBytes = decryptKey.GetBytes(keySize / 8); //specify key
                using (RijndaelManaged algorithmAES = new RijndaelManaged())  //create rijandaelmanged object
                {
                    algorithmAES.BlockSize = 256; //set block size
                    algorithmAES.Mode = CipherMode.CBC; //set cipher mode
                    algorithmAES.Padding = PaddingMode.PKCS7; //set padding
                    using (ICryptoTransform decryptor = algorithmAES.CreateDecryptor(keyBytes, ivBytes)) //create decryptor
                    using (MemoryStream memoryStream = new MemoryStream(cipherBytes)) //create memory stream
                    using (CryptoStream memStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)) //create crypto stream
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

      
        public static string EncryptPassword(string decryptKey) //encrypt password for login register
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