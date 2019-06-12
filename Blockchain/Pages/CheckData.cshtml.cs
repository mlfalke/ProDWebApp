using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blockchain.Pages
{
    public class CheckDataModel : PageModel
    {
        public string surname { get; set; }

        public string bsn { get; set; }

        public string birthDate { get; set; }

        public string antecedenten { get; set; }

        public string aanhoudingen { get; set; }

        public string heeftISDMaatregel { get; set; }

        public string heeftOnderzoekRad { get; set; }

        public string sepots { get; set; }

        public string lopendeDossiers { get; set; }

        public string heeftUitkering { get; set; }

        public string meldingenRad { get; set; }

        public string zitInGroepsAanpak { get; set; }

        public string heeftIdBewijs { get; set; }

        public string heeftLopendTraject { get; set; }

        public string laatsteGesprek { get; set; }
    }
}
