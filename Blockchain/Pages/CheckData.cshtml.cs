using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blockchain.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blockchain.Pages
{
    public class CheckDataModel : PageModel
    {
        [ViewData]
        public string surname { get; set; } = "Rick Sanchez";

        public string bsn { get; set; } = "None of your Business";

        public string birthDate { get; set; } = "Some Multiverses ago";

        public string type { get; set; } = "Wabalabadubdub";

        public string value { get; set; } = "L..L..L..Lick mah ballzz";

        public IActionResult OnGet()
        {
            CheckDataModel dataBlock = new CheckDataModel
            {
                surname = "Rick Sanchez",
                bsn = "None of your Business",
                birthDate = "Some Multiverses ago",
                type = "Wabalabadubdub",
                value = "L..L..L..Lick mah ballzz"
            };

            ViewData["CheckData"] = dataBlock;
            return Page();
        }

    }
}
