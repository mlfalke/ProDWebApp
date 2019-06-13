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

        public string type { get; set; }

        public string value { get; set; }
    }
}
