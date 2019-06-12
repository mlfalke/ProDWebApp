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
        [MaxLength(8)]
        [Display(Name = "antecedenten")]
        public string antecedenten { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Name = "aanhoudingen")]
        public string aanhoudingen { get; set; }

        public string heeftISDMaatregel { get; set; }

        public string heeftOnderzoekRad { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Name = "sepots")]
        public string sepots { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Name = "lopendeDossiers")]
        public string lopendeDossiers { get; set; }

        public string heeftUitkering { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Name = "meldingenRad")]
        public string meldingenRad { get; set; }

        public string zitInGroepsAanpak { get; set; }

        public string heeftIdBewijs { get; set; }

        public string heeftLopendTraject { get; set; }

        [Required]
        [MaxLength(16)]
        [Display(Name = "laatsteGesprek")]
        public string laatsteGesprek { get; set; }
    }
}
