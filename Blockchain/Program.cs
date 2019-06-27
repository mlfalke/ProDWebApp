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
        public static int port = 0;
        public static string name = "Unknown";
        public static string ipV4Address;
        public static P2PServer server = null;

        public static Boolean FirstRunServer = true;
        public static P2PClient client = new P2PClient();
        public static Server serversList;
        public static List<Company> companyList;
        public static Blockchainblock governmentChain = new Blockchainblock();
        private static Boolean tryConnect = false;

        public static Company hostCompany;


        public static void Main(string[] args)
        {
            using (StreamReader r = new StreamReader("companies.json"))
            {
                string companiesJson = r.ReadToEnd();
                companyList = JsonConvert.DeserializeObject<List<Company>>(companiesJson);
            }

            using (StreamReader r = new StreamReader("Server.json"))
            {
                string serverJson = r.ReadToEnd();
                serversList = JsonConvert.DeserializeObject<Server>(serverJson);
                ipV4Address = serversList.Ip;
                port = serversList.Port;
            }
            
            foreach (Company c in companyList)
            {
                if (c.name == serversList.Name)
                {
                    hostCompany = c;
                    break;
                }
            }
            
            server = new P2PServer();
            server.Start();
            ConnectServers();

            CreateWebHostBuilder(args).Build().Run();
            
           
        }

         static void ConnectServers()
        {
            foreach (var item in serversList.Nodes)
            {
                try
                {
                    if (client.Connect($"ws://{item.Ip}:{item.Port}/Blockchain")==false)
                    {
                        tryConnect = true;
                    };
                }
                catch (System.Exception Error)
                {
                    Console.WriteLine(Error);
                }
            }

            if (tryConnect)
            {
                tryConnect = false;
                // Create a timer with a two second interval.
                aTimer = new System.Timers.Timer();
                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = false;
                aTimer.Interval = 10000;
                aTimer.Enabled = true;
            }
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                            e.SignalTime);
            ConnectServers();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
