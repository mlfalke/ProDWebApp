using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
namespace Blockchain.Models.PeerToPeer{
    public class P2PClient
    {
        IDictionary<string, WebSocket> wsDict = new Dictionary<string, WebSocket>();

        public Boolean Connect(string url)
        {
            if (!wsDict.ContainsKey(url))
            {
                WebSocket ws = new WebSocket(url);
                ws.OnMessage += (sender, e) => 
                {
                    if (e.Data == "Hi Client")
                    {
                        Console.WriteLine(e.Data);
                    }
                    else
                    {
                        Blockchainblock newChain = JsonConvert.DeserializeObject<Blockchainblock>(e.Data);
                        if (newChain.IsValid() && newChain.Chain.Count > Blockchain.governmentChain.Chain.Count)
                        {
                            Blockchain.governmentChain = newChain;
                        }
                    }
                };
                    ws.Connect();
                    if(ws.IsAlive){
                        ws.Send("Hi Server");
                        ws.Send(JsonConvert.SerializeObject(Blockchain.governmentChain));
                        wsDict.Add(url, ws);
                        
                        ws.OnClose += (sender,e) =>
                        {
                            wsDict.Remove(ws.Url.ToString());
                        };

                        return true;
                    }else{
                        return false;
                    }
                   
            }
            return true;
        }

        public void Send(string url, string data)
        {
            foreach (var item in wsDict)
            {
                if (item.Key == url)
                {
                    item.Value.Send(data);
                }
            }
        }

        public void Broadcast(string data)
        {
            foreach (var item in wsDict)
            {
                item.Value.Send(data);
            }
        }

        public IList<string> GetServers()
        {
            IList<string> servers = new List<string>();
            foreach (var item in wsDict)
            {
                servers.Add(item.Key);
            }
            return servers;
        }

        public void Close()
        {
            foreach (var item in wsDict)
            {
                item.Value.Close();
            }
        }
    }
}
