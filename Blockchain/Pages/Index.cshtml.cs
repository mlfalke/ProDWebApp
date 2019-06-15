using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Blockchain.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            if (Models.LoadBlockchain.GovernmentChain != null)
            {
                //To do:
                // - Something has to happen when chain is invalid
                // - (Possibly with hashing) check if own GovernmentChain.txt is same as others
                if (! Models.LoadBlockchain.GovernmentChain.IsValid())
                {
                    //What happens when it's invalid?
                }
            }
            //If GovernmentChain doesn't exist, create new:
            using (StreamReader r = new StreamReader(@"GovernmentChain.json"))
            {
                string chainJson = r.ReadToEnd();
                Models.LoadBlockchain.GovernmentChain = JsonConvert.DeserializeObject<Models.Blockchainblock>(chainJson);
            }
        }
    }
}
