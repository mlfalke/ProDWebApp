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
            wss = new WebSocketServer($"ws://{Blockchain.ipV4Address}:{Blockchain.port}");
            // ws = new WebSocketServer(Program.Port);
            wss.AddWebSocketService<P2PServer>("/Blockchain");
            wss.Start();
            Console.WriteLine($"Started server at ws://{Blockchain.ipV4Address}:{Blockchain.port}");
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

                if (newChain.IsValid() && newChain.Chain.Count > Blockchain.governmentChain.Chain.Count)
                {
                    Blockchain.governmentChain = newChain;
                }

                if (!chainSynched)
                {
                    Send(JsonConvert.SerializeObject(Blockchain.governmentChain));
                    chainSynched = true;
                }
            }
        }



    }
}
