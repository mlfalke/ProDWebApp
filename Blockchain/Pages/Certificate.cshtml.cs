using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blockchain.Models.Cryptography;
using System.ComponentModel.DataAnnotations;

namespace Blockchain.Pages
{
    public class CertificateModel : PageModel
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public DateTime issuedon { get; set; }
        [Required]
        public DateTime issuedtill { get; set; }

        
       
        public async Task<IActionResult> OnPostCertificate(string name, DateTime issuedon, DateTime issuedtill, string password)
        {
            
            Key.Create(name, issuedon, issuedtill, password);
            return RedirectToPage();
        }
    }
}