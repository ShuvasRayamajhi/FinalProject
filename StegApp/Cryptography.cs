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

        public static string EncryptPassword(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();//encrypt the password
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder(); //Create string 
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

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
                        outputTxt = Convert.ToBase64String(memStream.ToArray()); //generate cypher text and return
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
                    throw new ArgumentNullException("Empty input text."); //error messages
                if (password == null)
                    throw new ArgumentNullException("Empty password.");
                if (algorithmAES != null)
                    algorithmAES.Clear();
            }

            return outputTxt; //return the encrypted text
        }

        public static string Decryption(string encryptedText, string password) //encrypted text, password
        {
            string decryptedText = "";
            RijndaelManaged algorithmAES = null; //to decrypt, initialise the rijndaelmanged object

            if (password != "" || encryptedText != "") //if both inputs are not empty
            {
                try
                {
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, _salt); //generate key 
                    byte[] bytes = Convert.FromBase64String(encryptedText); //create the stream to decrypt string
                    using (MemoryStream decryptMS = new MemoryStream(bytes))
                    {
                        algorithmAES = new RijndaelManaged(); //create rijandaelmanged object
                        algorithmAES.Key = key.GetBytes(algorithmAES.KeySize / 8); //specify key
                        algorithmAES.IV = ReadByteArray(decryptMS); //specify iv
                        ICryptoTransform decrypt = algorithmAES.CreateDecryptor(algorithmAES.Key, algorithmAES.IV); //create decryptor
                        using (CryptoStream decryptCs = new CryptoStream(decryptMS, decrypt, CryptoStreamMode.Read))
                        using (StreamReader srDecrypt = new StreamReader(decryptCs))
                            decryptedText = srDecrypt.ReadToEnd(); //read the decrypted btyes and assign them to a variable
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
                    Console.WriteLine("Empty input text."); //error message
                if (password == "")
                    Console.WriteLine("Empty password.");
                if (algorithmAES != null) //clear object
                    algorithmAES.Clear();
            }
            return decryptedText;
        }
        private static byte[] ReadByteArray(Stream stream) //reading bytes for decryption
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



