using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockchain.Pages;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Controllers
{
    public class AddBlockController : Controller
    {
        public IActionResult AddData()
        {
            return View();
        }

        public IActionResult AddData(AddDataModel ad)
        {
            var data = new blockdata
            {
                surname = ad.surname,
                bsn = ad.bsn,
                birthDate = ad.birthDate,
                antecedenten = ad.antecedenten,
                aanhoudingen = ad.aanhoudingen,
                heeftISDMaatregel = ad.heeftISDMaatregel,
                heeftOnderzoekRad = ad.heeftOnderzoekRad,
                sepots = ad.sepots,
                lopendeDossiers = ad.lopendeDossiers,
                heeftUitkering = ad.heeftUitkering,
                meldingenRad = ad.meldingenRad,
                zitInGroepsAanpak = ad.zitInGroepsAanpak,
                heeftIdBewijs = ad.heeftIdBewijs,
                heeftLopendTraject = ad.heeftLopendTraject,
                laatsteGesprek = ad.laatsteGesprek
            };

            return View();
        }

        public class blockdata
        {
            internal string surname;
            internal string bsn;
            internal string birthDate;
            internal string antecedenten;
            internal string aanhoudingen;
            internal string heeftISDMaatregel;
            internal string heeftOnderzoekRad;
            internal string sepots;
            internal string lopendeDossiers;
            internal string heeftUitkering;
            internal string meldingenRad;
            internal string zitInGroepsAanpak;
            internal string heeftIdBewijs;
            internal string heeftLopendTraject;
            internal string laatsteGesprek;
        }
    }
}