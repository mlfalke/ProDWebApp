using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Blockchain.Models
{
    public class Data {
        public string type;
        public string value;
        public Person person;

        public Data(string type, string value, Person person){
            this.type = type;
            this.value = value;
            this.person = person;
        }
    }
}