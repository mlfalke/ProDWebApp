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
           
            var x509 = new X509Certificate2(cert);
            

            using (RSA rsa = x509.GetRSAPublicKey())
            {

                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] Bdata = ByteConverter.GetBytes(data);

                

                byte[] encrypteddata = rsa.Encrypt(Bdata,RSAEncryptionPadding.Pkcs1);
                string encrypteddatas = Convert.ToBase64String(encrypteddata);
                return encrypteddatas;
            }
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
            string targetDirectory = @"Models/Encryption/Certificates";
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            List<string> lijst = new List<string>();
            foreach (string fileName in fileEntries) {
                lijst.Add(fileName);
            }
            return lijst;
            

        }

        // Insert logic for processing found files here.
        public static string ProcessFile(string certDirectory)
        {
            string targetDirectory =Path.Combine(@"Models/Encryption/Certificates/" +certDirectory);
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            string certName = fileEntries[0];
            return certName;
        }


        public static string Prikey()
        {
            string targetDirectory = @"Models/Encryption/CertPrivate";
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            return( fileEntries[0] );
        }


    }
}