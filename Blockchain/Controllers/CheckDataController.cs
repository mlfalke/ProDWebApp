using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockchain.Models;
using Blockchain.Pages;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Controllers
{
    public class CheckDataController : Controller
    {
        [HttpGet]
        public ActionResult CheckData()
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
            return View();
        }
    }
}