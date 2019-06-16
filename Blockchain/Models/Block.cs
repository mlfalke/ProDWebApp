using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Blockchain.Models.Cryptography;
using Newtonsoft.Json;

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

        X509Certificate cert = X509Certificate.CreateFromSignedFile(cert2);
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        //Define variable 'data' that will be encrypted later
        string data = "{'value': "+newData.value+", 'person': {'surname': '"+person.surname+"', 'bsn': '"+person.bsn+"', 'birthDate': '"+person.birthDate.ToString()+"'}}";
        //Change data to byte[] in order to encrypt it later
        byte[] Bdata = ByteConverter.GetBytes(data);
        string blockData = "{'type': '"+newData.type+"', 'sender': '"+hostCompany.name+"', 'value': [";
        
        //Check if companies have permission to read the data. If so, encrypt the data with their keys and add it to JSON-formatted string blockData
        foreach(Company c in companies){
            foreach(Permission p in c.GetTruePermissions()){
                if(p.name == newData.type){
                    //TO DO: Add gathering of specific public key of the company (c) with "c.publicKey" and encrypt variable "data" with that key.
                    blockData = blockData + "{'targetCompany': '"+c.name+"', 'Data': '"+Encryption.DataEncrypt(data,cert)+"'},";
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
            var Data = JsonConvert.DeserializeObject<dynamic>(this.Data);
            string allData = Data["value"]["data"];
            var allDataJson = JsonConvert.DeserializeObject<dynamic>(allData);
            string data = "";
            foreach (dynamic d in allDataJson)
            {
                if (d["targetCompany"] == Blockchain.hostCompany)
                {
                    data = d["data"];
                }
            }
            string Ddata = data;
            Data dataObject = JsonConvert.DeserializeObject<Data>(Ddata);
            dataObject.type = Data["type"];
            return (dataObject);
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