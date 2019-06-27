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

        X509Certificate cert = new X509Certificate(cert2);
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        //Define variable 'data' that will be encrypted later

        string data = JsonConvert.SerializeObject(newData); 
        // JsonConvert.SerializeObject(new Data("Politie","HAllo",person));
        // "{'value': " + newData.value + ", 'person': {'surname': '" + person.surname + "', 'bsn': '" + person.bsn + "', 'birthDate': '" + person.birthDate.ToString() + "'}}";

        string blockData = "{'type': '"+newData.type+"', 'sender': '"+hostCompany.name+"', 'encryptedValues': [";
        
        //Check if companies have permission to read the data. If so, encrypt the data with their keys and add it to JSON-formatted string blockData
        foreach(Company c in companies){
            foreach(Permission p in c.GetTruePermissions()){
                if(p.name == newData.type){
                        //TO DO: Add gathering of specific public key of the company (c) with "c.publicKey" and encrypt variable "data" with that key.
                        blockData = blockData + "{'targetCompany': '"+c.name+"', 'encryptedData': '"+Encryption.DataEncrypt(data,cert)+"'},";
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
                        X509Certificate2 cert = new X509Certificate2(@"Models/Encryption/CertPrivate/B46686F98B414B867550AAB0CCAE2EB4A8733937.pfx", "1234",X509KeyStorageFlags.Exportable);
                        string decryptedDataString = Encryption.DataDecrypt(eV.encryptedData, cert);
                        Data decryptedData = JsonConvert.DeserializeObject<Data>(decryptedDataString);
                        decryptedData.type = Data.type;
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