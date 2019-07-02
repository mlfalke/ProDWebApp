using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Blockchain.Models.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Blockchain.Models
{   
    public class Block  
{  
    public int Index { get; set; }  
    public string TimeStamp { get; set; }  
    public string PreviousHash { get; set; }  
    public string Hash { get; set; }  
    public Blockdata Data { get; set; }


        [JsonConstructor]
    public Block(DateTime timeStamp){
        this.Index = 0;
        this.TimeStamp =  timeStamp.ToString("MM/dd/yyyy hh:mm tt");
        this.PreviousHash = "";
        this.Hash = CalculateHash();
    }
    
    public Block(DateTime timeStamp, Data newData, List<Company> companies, Company hostCompany)  
    {
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        //Define variable 'data' that will be encrypted later

        string data = JsonConvert.SerializeObject(newData); 
        Blockdata blockData = new Blockdata(newData.type,hostCompany.name); 
        //Check if companies have permission to read the data. If so, encrypt the data with their keys and add it to JSON-formatted string blockData
        foreach(Company c in companies){
            Data encryptedData = new Data(newData.type,newData.value,new Person(newData.person.surname,newData.person.bsn,newData.person.birthDate));
            foreach(Permission p in c.GetTruePermissions()){
                if(p.name == newData.type){
                    string certName = Encryption.ProcessFile(c.name);
                    X509Certificate cert = new X509Certificate(certName);

                    //TO DO: Add gathering of specific public key of the company (c) with "c.publicKey" and encrypt variable "data" with that key.
                    encryptedData.value = Encryption.DataEncrypt(encryptedData.value,cert);
                    encryptedData.person.birthDate = Encryption.DataEncrypt(encryptedData.person.birthDate,cert);
                    encryptedData.person.bsn = Encryption.DataEncrypt(encryptedData.person.bsn,cert);
                    encryptedData.person.surname = Encryption.DataEncrypt(encryptedData.person.surname,cert);

                    blockData.encryptedValues.Add(new EncryptedValue(c.name,encryptedData));
                }
            }
            encryptedData = null; 
        }
        this.Index = 0;  
        this.TimeStamp = timeStamp.ToString("MM/dd/yyyy hh:mm tt");;  
        this.PreviousHash = "";  
        this.Data = blockData;
        this.Hash = CalculateHash();  
    }

        public Data GetBlockData()
        {
            if(this.Data!=null)
            {
                Blockdata Data = this.Data; 
                List<EncryptedValue> encryptedValues = Data.encryptedValues;
                foreach(EncryptedValue eV in encryptedValues){
                    if(eV.targetCompany == Blockchain.hostCompany.name){

                        X509Certificate2 cert = new X509Certificate2(Encryption.Prikey(), Blockchain.myPassword,X509KeyStorageFlags.Exportable);
                        
                        Data decryptedData = new Data(eV.encryptedData.type,eV.encryptedData.value,new Person(eV.encryptedData.person.surname,eV.encryptedData.person.bsn,eV.encryptedData.person.birthDate)); 
                        decryptedData.value = Encryption.DataDecrypt(decryptedData.value,cert);
                        decryptedData.person.birthDate = Encryption.DataDecrypt(decryptedData.person.birthDate,cert);
                        decryptedData.person.bsn = Encryption.DataDecrypt(decryptedData.person.bsn,cert);
                        decryptedData.person.surname = Encryption.DataDecrypt(decryptedData.person.surname,cert);
                        decryptedData.type = Data.type;
                        return(decryptedData);
                    }
                }
                return encryptedValues[0].encryptedData;
            }
            return new Data("","",new Person("empty","empty",this.TimeStamp.ToString()));
        }

    public string CalculateHash()  
    {  
        SHA256 sha256 = SHA256.Create();  
        // Hash word aangemaakt voordat data is gencrypt.
        byte[] inputBytes = Encoding.ASCII.GetBytes($"{this.TimeStamp}-{this.PreviousHash ?? ""}-{this.Data}");  
        byte[] outputBytes = sha256.ComputeHash(inputBytes);  
  
        return Convert.ToBase64String(outputBytes);  
    }  
}
}