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

namespace Blockchain
{
    public class Blockchain
    {
        private static System.Timers.Timer aTimer;
        public static int Port = 0;
        public static string name = "Unknown";
        public static string IpV4Address;
        public static P2PServer Server = null;
        public static P2PClient Client = new P2PClient();
        private static Boolean tryConnect = false;


        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
