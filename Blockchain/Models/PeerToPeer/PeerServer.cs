using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;


namespace Blockchain.Models.PeerToPeer
{
    public class P2PServer : WebSocketBehavior
    {
        bool chainSynched = false;
        WebSocketServer wss = null;
        WebSocketServer ws = null;

        public void Start()
        {
            wss = new WebSocketServer($"wss://{Blockchain.IpV4Address}:{Blockchain.Port}");
            // ws = new WebSocketServer(Program.Port);
            wss.AddWebSocketService<P2PServer>("/Blockchain");
            wss.Start();
            Console.WriteLine($"Started server at ws://{Blockchain.IpV4Address}:{Blockchain.Port}");
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Hi Server")
            {
                Console.WriteLine(e.Data);
                Send("Hi Client");
            }
            else
            {
                Blockchainblock newChain = JsonConvert.DeserializeObject<Blockchainblock>(e.Data);

                if (newChain.IsValid() && newChain.Chain.Count > LoadBlockchain.GovernmentChain.Chain.Count)
                {
                    List<Transaction> newTransactions = new List<Transaction>();
                    // newTransactions.AddRange(newChain.PendingTransactions);
                    // newTransactions.AddRange(Program.GovernmentChain.PendingTransactions);

                    // newChain.PendingTransactions = newTransactions;
                    LoadBlockchain.GovernmentChain = newChain;
                }

                if (!chainSynched)
                {
                    Send(JsonConvert.SerializeObject(LoadBlockchain.GovernmentChain));
                    chainSynched = true;
                }
            }
        }



    }
}
