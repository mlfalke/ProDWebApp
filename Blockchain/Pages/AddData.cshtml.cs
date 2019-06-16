using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blockchain.Models.Cryptography;
using Blockchain.Models;
using Newtonsoft.Json;

namespace Blockchain.Pages
{
    public class AddDataModel : PageModel
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "surname")]
        public string surname { get; set; }

        [Required]
        [MinLength(9, ErrorMessage = "BSN is te kort")]
        [MaxLength(9, ErrorMessage = "BSN is te lang")]
        [Display(Name = "bsn")]
        public string bsn { get; set; }

        [Required]
        [MaxLength(16)]
        [Display(Name = "birthDate")]
        public DateTime birthDate { get; set; }

        [Required]
        [Display(Name = "type")]
        public string type { get; set; }

        [Required]
        [Display(Name = "value")]
        public string value { get; set; }

        public List<string> cert = Encryption.ProcessFile();

        public IActionResult OnPostAsync([FromForm]string surname, string bsn, DateTime birthDate, string type, string value, string cert)
        {

            Person person = new Person(surname, bsn, birthDate);
            Data newData = new Data(type, value, person);
            // LoadBlockchain.loadchain();
            
            Block block = new Block(DateTime.Now, newData, person, Blockchain.companyList, Blockchain.hostCompany,cert);
            
            Blockchain.governmentChain.AddBlock(block);
            
            Blockchain.client.Broadcast(JsonConvert.SerializeObject(Blockchain.governmentChain));

            return RedirectToPage();
        }
    }
}
