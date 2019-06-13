using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public string birthDate { get; set; }

        [Required]
        [Display(Name = "type")]
        public string type { get; set; }

        [Required]
        [Display(Name = "value")]
        public string value { get; set; }
    }
}
