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
    public class Blockdata
    
    {
        public string type {get; set;}
        public string sender {get; set;}
        public List<EncryptedValue> encryptedValues {get; set;}

        public Blockdata(string type,string sender){
            this.type= type;
            this.sender = sender;
            this.encryptedValues = new List<EncryptedValue>();
        }
    }
}