﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blockchain.Models.Cryptography;
using Blockchain.Models;

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


        public async Task<IActionResult> OnPostAsync(string surname, string bsn, DateTime birthDate, string type, string value)
        {

            Person person = new Person(surname, bsn, birthDate);
            Data newData = new Data(type, value, person);
            Block block = new Block(DateTime.Now, newData, person, Program.companies, Program.hostCompany);
            Program.GovernmentChain.AddBlock(block);

            return RedirectToPage("/CheckData");
        }
    }
}
