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
    public class PassCheckModel : PageModel
    {
        public string passwordcheck { get; set; }

        public IActionResult OnPost(string passwordcheck)
         {
            if (Blockchain.CheckPassword( passwordcheck))
            {
                return RedirectToPage("CheckData");
            }
            else
            {
                return RedirectToPage("PassCheck");
            }
            
         }
    }
}
