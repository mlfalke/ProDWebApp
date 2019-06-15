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
        [Display(Name = "name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "password")]
        public string password { get; set; }
        [Required]
        [Display(Name = "issuedon")]
        public DateTime issuedon { get; set; }
        [Required]
        [Display(Name = "issuedtill")]
        public DateTime issuedtill { get; set; }


        
        public IActionResult OnPost([FromForm] string name, DateTime issuedon, DateTime issuedtill, string password)
        {
            

            Key.Create(name, issuedon, issuedtill, password);
            return RedirectToPage();
        }
    }
}