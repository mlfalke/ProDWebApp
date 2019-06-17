using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Blockchain.Models.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blockchain.Models
{   
    public class Block  
{  
    public int Index { get; set; }  
    public DateTime TimeStamp { get; set; }  
    public string PreviousHash { get; set; }  
    public string Hash { get; set; }  
    public string Data { get; set; }


        [JsonConstructor]
    public Block(DateTime timeStamp){
        this.Index = 0;
        this.TimeStamp = timeStamp;
        this.PreviousHash = "";
        this.Data = "{}";
        this.Hash = CalculateHash();
    }
    
    public Block(DateTime timeStamp, Data newData, Person person, List<Company> companies, Company hostCompany, string cert2)  
    {

            X509Certificate2 cert = new X509Certificate2(cert2, "Wachtwoord");
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        //Define variable 'data' that will be encrypted later
        //Change data to byte[] in order to encrypt it later
        byte[] Bdata = ByteConverter.GetBytes(data);
        string blockData = "{'type': '"+newData.type+"', 'sender': '"+hostCompany.name+"', 'encryptedValues': [";
        
        //Check if companies have permission to read the data. If so, encrypt the data with their keys and add it to JSON-formatted string blockData
        foreach(Company c in companies){
            foreach(Permission p in c.GetTruePermissions()){
                if(p.name == newData.type){
                        //TO DO: Add gathering of specific public key of the company (c) with "c.publicKey" and encrypt variable "data" with that key.
                        string data = "{'value': " + Encryption.DataEncrypt(newData.value, cert) + ", 'person': {'surname': '" + Encryption.DataEncrypt(person.surname, cert) + "', 'bsn': '" + Encryption.DataEncrypt(person.bsn, cert) + "', 'birthDate': '" + Encryption.DataEncrypt(person.birthDate.ToString(), cert) + "'}}";
                        blockData = blockData + "{'targetCompany': '"+c.name+"', 'encryptedData': '"+data+"'},";
                }
            }
        }

        

        //Close and finish the blockData string so it is fully closed off
        blockData = blockData.Substring(0,blockData.Length-1);
        blockData = blockData + "]}";

        this.Index = 0;  
        this.TimeStamp = timeStamp;  
        this.PreviousHash = "";  
        this.Data = blockData;
        this.Hash = CalculateHash();  
    }

        public Data GetBlockData()
        {
            if(this.Data!="{}")
            {
                Blockdata Data = JsonConvert.DeserializeObject<Blockdata>(this.Data);
                // <dynamic>(this.Data);
                EncryptedValue[] encryptedValues = Data.encryptedValues;
                foreach(EncryptedValue eV in encryptedValues){
                    if(eV.targetCompany == Blockchain.hostCompany.name){
                        X509Certificate2 cert = new X509Certificate2(@"C:\Users\matth\Documents\Gitrepo\Blockchain\Blockchain_FINAL_PROJECT\ProDWebApp\Blockchain\Models\Encryption\CertPrivate\EE06F0A054F223238D34D7320F0C7B33DBDC2D7D.pfx","Wachtwoord",X509KeyStorageFlags.Exportable);
                        Data encryptedData = JsonConvert.DeserializeObject<Data>(eV.encryptedData);
                        Data decryptedData = new Data(Data.type, Encryption.DataDecrypt(encryptedData.value, cert), new Person(Encryption.DataDecrypt(encryptedData.person.surname, cert), Encryption.DataDecrypt(encryptedData.person.bsn, cert), Convert.ToDateTime(Encryption.DataDecrypt(encryptedData.person.birthDate.ToString(), cert))));
                        return(decryptedData);
                    }        
                }               
            }
            return new Data("","",new Person("empty","empty",DateTime.Now));
            
        }



        public string CalculateHash()  
    {  
        SHA256 sha256 = SHA256.Create();  
  
        byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Data}");  
        byte[] outputBytes = sha256.ComputeHash(inputBytes);  
  
        return Convert.ToBase64String(outputBytes);  
    }  
}
}