using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace StegApp
{
    class Cryptography
    {
        private static byte[] _salt = Encoding.ASCII.GetBytes("fdsklk23rjfe9arejk23rnboamep");

        public static string Encryption(string inputTxt, string password)
        {
            string outputTxt = ""; //output encrypted string
            RijndaelManaged algorithmAES = null; //initialise the object

            if (password != "" || inputTxt != "") //check the text to encrypt and password are not empty
            {
                algorithmAES = new RijndaelManaged();  //create RijndaelManaged object (used to encrypt text)
                Rfc2898DeriveBytes decryptKey = new Rfc2898DeriveBytes(password, _salt); //generate the  decryption key using the inupt password
                algorithmAES.Key = decryptKey.GetBytes(algorithmAES.KeySize / 8); //set the decryption key size
                ICryptoTransform encrypt = algorithmAES.CreateEncryptor(algorithmAES.Key, algorithmAES.IV);//create the encryptor for the stream
                try
                {
                    using (MemoryStream memStream = new MemoryStream()) //create memory stream for encryption
                    {
                        memStream.Write(BitConverter.GetBytes(algorithmAES.IV.Length), 0, sizeof(int)); //write
                        memStream.Write(algorithmAES.IV, 0, algorithmAES.IV.Length);
                        using (CryptoStream EncryptCs = new CryptoStream(memStream, encrypt, CryptoStreamMode.Write))
                        using (StreamWriter EncryptSw = new StreamWriter(EncryptCs))
                            EncryptSw.Write(inputTxt); //write the plain text to the stream
                        outputTxt = Convert.ToBase64String(memStream.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                if (inputTxt == null)
                    throw new ArgumentNullException("Empty input text.");
                if (password == null)
                    throw new ArgumentNullException("Empty password.");
                if (algorithmAES != null)
                    algorithmAES.Clear();
            }

            return outputTxt; //return the encrypted text
        }
        public static string Decryption(string encryptedText, string password) //encrypted encryptedText
        {
            string decryptedText = "";
            RijndaelManaged algorithmAES = null;

            if (password != "" || encryptedText != "")
            {
                try
                {
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, _salt);
                    byte[] bytes = Convert.FromBase64String(encryptedText);
                    using (MemoryStream decryptMS = new MemoryStream(bytes))
                    {
                        algorithmAES = new RijndaelManaged();
                        algorithmAES.Key = key.GetBytes(algorithmAES.KeySize / 8);
                        algorithmAES.IV = ReadByteArray(decryptMS);
                        ICryptoTransform decrypt = algorithmAES.CreateDecryptor(algorithmAES.Key, algorithmAES.IV);
                        using (CryptoStream decryptCs = new CryptoStream(decryptMS, decrypt, CryptoStreamMode.Read))
                        using (StreamReader srDecrypt = new StreamReader(decryptCs))
                            decryptedText = srDecrypt.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                if (encryptedText == "")
                    Console.WriteLine("Empty input text.");
                if (password == "")
                    Console.WriteLine("Empty password.");
                if (algorithmAES != null)
                    algorithmAES.Clear();
            }
            return decryptedText;
        }
        private static byte[] ReadByteArray(Stream stream)
        {
            byte[] lengthRaw = new byte[sizeof(int)];
            if (stream.Read(lengthRaw, 0, lengthRaw.Length) != lengthRaw.Length)
                Console.WriteLine("Contains bad array.");
            byte[] buffer = new byte[BitConverter.ToInt32(lengthRaw, 0)];
            if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                Console.WriteLine("Cannot read.");
            return buffer;
        }
    }

}
//References "CryptoStream Class" Microsoft (n.d.) https://docs.microsoft.com/


