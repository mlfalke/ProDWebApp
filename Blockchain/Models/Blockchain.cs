using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Blockchain.Models
{

    // public class LoadBlockchain
    // {
    //     public static List<Company> companies { get;  set; }
    //     public static Company hostCompany { get;  set; }
    //     public static Server Serverslist;
    //     public static Blockchainblock GovernmentChain { get; set; }
    //     public static System.Security.Cryptography.X509Certificates.X509Certificate2 certificate { get; set; }

    //     public static void loadchain()
    //     {
    //         // GovernmentChain = new Blockchainblock();
    //         //FileStream filestreamCreate = new FileStream("Blockchain.txt", FileMode.OpenOrCreate);

    //         using (StreamReader r = new StreamReader("companies.json"))
    //         {
    //             string companiesJson = r.ReadToEnd();
    //             companies = JsonConvert.DeserializeObject<List<Company>>(companiesJson);
    //         }

    //         //Change to loadServerJson() (server output)
    //         using (StreamReader r = new StreamReader("Server.json"))
    //         {
    //             string serverJson = r.ReadToEnd();
    //             Serverslist = JsonConvert.DeserializeObject<Server>(serverJson);
    //         }
    //         //Search in lists of companies for host name to get hostCompany
    //         foreach (Company c in companies)
    //         {
    //             if (c.name == Serverslist.Name)
    //             {
    //                 hostCompany = c;
    //                 break;
    //             }
    //         }
    //     }
    // }
    public class Blockchainblock
        {

            public IList<Block> Chain { get; set; }


            [JsonConstructor]
            public Blockchainblock()
            {
                // GetCurrentChain();

            }
            #region 

            // public IList<Block> GetCurrentChain()
            // {
            //     //var dutu = @"Blockchain\Blockchain.txt";
            //     //var fileReader = new System.IO.StreamReader("Blockchain.txt");
            //     List<string> StringsFromFile = new List<string>();
            //     //string lineOfText;
            //     Chain = new List<Block>();
            //     //JsonTextReader reader = new JsonTextReader(fileReader);
            //     using (StreamReader file = File.OpenText("Blockchain.txt"))
            //     {
            //         var settings = new JsonSerializerSettings()
            //         {
            //             TypeNameHandling = TypeNameHandling.All
            //         };
            //         JsonSerializer serializer = new JsonSerializer();
            //         Block block = (Block)serializer.Deserialize(file, typeof(Block),settings);
            //         Chain.Add(block);
            //         Console.WriteLine(JsonConvert.SerializeObject(block));
            //     }

            //     return Chain;



            //     // while ((lineOfText = fileReader.ReadLine()) != null)
            //     // {
            //     //     StringsFromFile.Add(lineOfText);
            //     // }


            //     // //JsonConvert.DeserializeObject<Block>(reader);


            //     // foreach (var line in StringsFromFile)
            //     // {

            //     //     Console.WriteLine(line);
            //     //     string[] entries = line.Split("Chain:[" + "]");

            //     //     List<Block> block = JsonConvert.DeserializeObject<List<Block>>(entries[2]);
            //     //     //Console.WriteLine(block);
            //     // }



            // }
            #endregion

            public void InitializeChain()
            {
                Chain = new List<Block>();
                AddGenesisBlock();
            }

            public Block CreateGenesisBlock()
            {
                return new Block(DateTime.Now);
            }

            public void AddGenesisBlock()
            {
                Chain.Add(CreateGenesisBlock());
            }

            public Block GetLatestBlock()
            {
                return Chain[Chain.Count - 1];
            }



            public void AddBlock(Block block)
            {
                Block latestBlock = GetLatestBlock();
                block.Index = latestBlock.Index + 1;
                block.PreviousHash = latestBlock.Hash;
                block.Hash = block.CalculateHash();
                Chain.Add(block);
            }

            public IList<Block> GetAllBlocks()
            {
                return this.Chain;
            }

            public IList<Data> GetAllData()
            {
                var dataList = new List<Data>();
                foreach (Block b in this.Chain)
                {
                    dataList.Add(b.GetBlockData());
                }
                return (dataList);
            }   

            public IList<Block> GetBlocksByType(string type)
            {
                IList<Block> blocks = new List<Block>();
                foreach (Block b in Chain)
                {
                    // var dataJson = JsonConvert.DeserializeObject<dynamic>(b.Data);
                    if (b.Data.type == type)
                    {
                        blocks.Add(b);
                    }
                }
                return (blocks);
            }

            public IList<Block> GetBlocks(string type, Person person)
            {
                IList<Block> blocks = new List<Block>();
                foreach (Block b in Chain)
                {
                    // var dataJson = JsonConvert.DeserializeObject<dynamic>(b.Data);
                    if (b.Data.type == type && b.GetBlockData().person == person)
                    {
                        blocks.Add(b);
                    }
                }
                return (blocks);
            }
            public Block GetMostRecent(string type, Person person)
            {
                DateTime newestTime = DateTime.MinValue;
                Block newestBlock = new Block(DateTime.Now);
                var blocks = GetBlocks(type, person);
                foreach (Block b in blocks)
                {
                    if (b.TimeStamp > newestTime)
                    {
                        newestTime = b.TimeStamp;
                        newestBlock = b;
                    }
                }
                return (newestBlock);
            }

            public IList<Block> GetBlocksByPerson(Person person)
            {
                IList<Block> blocks = new List<Block>();
                foreach (Block b in Chain)
                {
                    if (b.GetBlockData().person == person)
                    {
                        blocks.Add(b);
                    }
                }
                return (blocks);
            }

        public bool IsValid()
            {
                for (int i = 1; i < Chain.Count; i++)
                {
                    Block currentBlock = Chain[i];
                    Block previousBlock = Chain[i - 1];
                    
                    if (currentBlock.Hash != currentBlock.CalculateHash())
                    {
                        return false;
                    }

                    if (currentBlock.PreviousHash != previousBlock.Hash)
                    {
                        return false;
                    }
                }
                return true;
            }

        }
    }
