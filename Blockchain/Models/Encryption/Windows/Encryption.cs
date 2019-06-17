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
        public static string DataEncrypt(string data, X509Certificate2 cert)
        {

            using (RSA rsa = cert.GetRSAPublicKey())
            {

                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] Bdata = ByteConverter.GetBytes(data);

                byte[] decryptedData = rsa.Encrypt(Bdata,RSAEncryptionPadding.OaepSHA256);
                string decryptedDataS = Convert.ToBase64String(decryptedData);
                return decryptedDataS;
            }
            
            
        }

        public static string DataDecrypt(string data, X509Certificate2 cert)
        {
            using (RSA rsa = cert.GetRSAPrivateKey())
            {
                
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] Bdata = Convert.FromBase64String(data);

                byte[] decryptedData = rsa.Decrypt(Bdata, RSAEncryptionPadding.OaepSHA256);
                string decryptedDataS = Convert.ToBase64String(decryptedData);
                return decryptedDataS;
            }

        }


        // Insert logic for processing found files here.
        public static List<string> ProcessFile()
        {
            string targetDirectory = @"Models/Encryption/CertPrivate";
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            List<string> lijst = new List<string>();
            foreach (string fileName in fileEntries) {
                lijst.Add(fileName);
            }
            return lijst;
            

        }


        public static List<string> Prikey()
        {
            string targetDirectory = @"Models/Encryption/CertPrivate";
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            List<string> lijst = new List<string>();
            foreach (string fileName in fileEntries)
            {
                lijst.Add(fileName);
            }
            return lijst;


        }


    }
}