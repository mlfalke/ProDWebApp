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
        public string encryptedData{get; set;}  
    }
}