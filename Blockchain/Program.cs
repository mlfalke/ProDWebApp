using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Timers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Blockchain.Models;
using Blockchain.Models.Cryptography;
using Blockchain.Models.PeerToPeer;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain
{
    public class Program
    {
        private static System.Timers.Timer aTimer;
        public static int Port = 0;
        public static string name = "Unknown";
        public static string IpV4Address;
        public static P2PServer Server = null;
        public static P2PClient Client = new P2PClient();
        private static Boolean tryConnect = false;

        public static List<Company> companies { get; private set; }
        public static Company hostCompany { get; private set; }
        public static Server Serverslist;
        public static Blockchain.Models.Blockchain GovernmentChain { get; set; }
        public static X509Certificate2 certificate { get; set; }

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            var password = "Hetwachtwoord";
            var collection = new X509Certificate2Collection();
            string pet = @"6FE9A3444993E4EB9D449D532FD7B4CDFD19B4BC.pfx";
            collection.Import(pet,password, X509KeyStorageFlags.PersistKeySet);
            certificate = collection[0];
            if (!certificate.HasPrivateKey)throw new Exception("The certificate does not have a private key");
            
            GovernmentChain = new Blockchain.Models.Blockchain();
            //FileStream filestreamCreate = new FileStream("Blockchain.txt", FileMode.OpenOrCreate);
            
            using (StreamReader r = new StreamReader("companies.json"))
            {
                string companiesJson = r.ReadToEnd();
                companies = JsonConvert.DeserializeObject<List<Company>>(companiesJson);
            }
            
            //Change to loadServerJson() (server output)
            using (StreamReader r = new StreamReader("Server.json"))
            {
                string serverJson = r.ReadToEnd();
                Serverslist = JsonConvert.DeserializeObject<Server>(serverJson);
            }
            //Search in lists of companies for host name to get hostCompany
            foreach( Company c in companies){
                if(c.name == Serverslist.Name){
                    hostCompany = c;
                    break;
                }
            }
        }

        public void CheckIsValid()
        {
            if(GovernmentChain != null)
            {
                //To do:
                // - Something has to happen when chain is invalid
                // - (Possibly with hashing) check if own GovernmentChain.txt is same as others
                if (!GovernmentChain.IsValid())
                {
                    //What happens when it's invalid?
                }
            }
            //If GovernmentChain doesn't exist, create new:
            using (StreamReader r = new StreamReader("GovernmentChain.txt"))
            {
                string chainJson = r.ReadToEnd();
                GovernmentChain = JsonConvert.DeserializeObject<Models.Blockchain>(chainJson);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
