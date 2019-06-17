using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle;

using System.Security.Permissions;
using System.Text;
using Newtonsoft.Json;

namespace Blockchain.Models.Cryptography
{
    public class Encryption
    {
        public static string DataEncrypt(string data, X509Certificate cert)
        {


            byte[] pubkey = cert.GetPublicKey();
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            //Create a new instance of RSAParameters.
            RSAParameters RSAKeyInfo = new RSAParameters();

            //Set RSAKeyInfo to the public key values. 
            RSAKeyInfo.Modulus = pubkey;
            RSAKeyInfo.Exponent = cert.GetKeyAlgorithmParameters();
            
            RSA.ImportParameters(RSAKeyInfo);
            
      
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] Bdata = ByteConverter.GetBytes(data);
            byte[] encryptedData = RSA.Encrypt(Bdata, RSAEncryptionPadding.Pkcs1);
            string encryptedDataS = Convert.ToBase64String(encryptedData);
            return encryptedDataS;
            
        }

        public static string DataDecrypt(string data, X509Certificate2 cert)
        {
            using (RSA rsa = cert.GetRSAPrivateKey())
            {
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] Bdata = Convert.FromBase64String(data);

                byte[] decryptedData = rsa.Decrypt(Bdata, RSAEncryptionPadding.Pkcs1);
                string decryptedDataS = ByteConverter.GetString(decryptedData);
                return decryptedDataS;
            }

        }


        // Insert logic for processing found files here.
        public static List<string> ProcessFile()
        {
            string targetDirectory = @"Models\Encryption\Certificates\";
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            List<string> lijst = new List<string>();
            foreach (string fileName in fileEntries) {
                lijst.Add(fileName);
            }
            return lijst;
            

        }


    }
}