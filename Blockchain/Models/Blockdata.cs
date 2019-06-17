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
        public EncryptedValue[] encryptedValues {get; set;}
    }
}