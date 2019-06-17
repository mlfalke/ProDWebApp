using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blockchain.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blockchain.Models.Cryptography;

namespace Blockchain.Pages
{
    public class CheckDataModel : PageModel
    {
        [ViewData]
        public string surname { get; set; }

        public string bsn { get; set; }

        public string birthDate { get; set; }

        public string type { get; set; }

        public string value { get; set; }
        public List<string> cert = Encryption.Prikey();

        public void OnGet() 
            {
            surname = "Rick Sanchez";
            bsn = "None of your Business";
            birthDate = "Some Multiverses ago";
            type = "Wabalabadubdub";
            value = "L..L..L..Lick mah ballzz";
            }        
    }
}
