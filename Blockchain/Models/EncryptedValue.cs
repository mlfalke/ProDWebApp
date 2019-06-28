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
    public class EncryptedValue  
    {
        public string targetCompany{get; set;}
        public Data encryptedData{get; set;}  

        public EncryptedValue(string targetCompany,Data encryptedData){
            this.targetCompany = targetCompany;
            this.encryptedData = encryptedData;
        }
    }
}