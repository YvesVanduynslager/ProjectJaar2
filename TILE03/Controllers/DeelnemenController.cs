using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TILE03.Models.Domain;
using TILE03.Models.Repositories;
using TILE03.Models.ViewModels;

namespace TILE03.Controllers
{
    public class DeelnemenController : Controller
    {
        private readonly ISessieRepository _sessieRepository;
        private readonly IGroepRepository _groepRepository;

        public DeelnemenController(ISessieRepository sessieRepository, IGroepRepository groepRepository)
        {
            _sessieRepository = sessieRepository;
            _groepRepository = groepRepository;
        }

        [HttpGet]
        //Startscherm om deel te nemen aan een sessie
        public IActionResult Index()
        {
            return View(new DeelnemenIndexViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Hier controleren we of de sessie geactiveerd is aan de hand van de ingegeven code
        public IActionResult ControleerSessiecode(DeelnemenIndexViewModel deelnemenIndexViewModel)
        {
            //Een sessiecode is geldig als het een geactiveerde sessie betreft
            Sessie sessie = _sessieRepository.GetByCode(deelnemenIndexViewModel.Code);

            if (sessie != null && sessie.StateType != 0)
            {
                return RedirectToAction(nameof(OpenSessie), new {id = sessie.Id});
            }
            else
            {
                ViewData["Error"] = "Deze sessie is nog niet geactiveerd";
                return View(nameof(Index));
            }
        }

        [HttpGet]
        //Dit is het aanmeldingsscherm waar de groepen hun groep kunnen aanduiden
        public IActionResult OpenSessie(int id)
        {
            Sessie sessie = _sessieRepository.GetById(id);
            return View(sessie);
        }       

        [HttpPost]
        [ValidateAntiForgeryToken]
        //De groep meldt zich aan en gaat door naar het InWacht scherm
        public IActionResult GroepAanmelden(int id, int groepId)
        {
            Groep groep = _groepRepository.GetById(groepId);
            if (groep.HuidigeState == "Niet Aangemeld")
            {
                groep.HuidigeState = "Aangemeld";
            }
            _groepRepository.SaveChanges();
            return RedirectToAction(nameof(InWacht),"Deelnemen",new { id = id, groepId = groepId });
        }

        [HttpGet]
        //Hier krijgt men het wachtscherm te zien zolang de sessie nog niet activeerd is, merk op refresh elke 15 seconden
        public IActionResult InWacht(int id, int groepId)
        {
            Sessie sessie = _sessieRepository.GetById(id);
            ViewData["groepId"] = groepId;
            Response.Headers.Add("Refresh", "15");
            return View(sessie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //als de sessie actief is, kan men de sessie starten en gaat men naar de uitvoerenController
        public IActionResult StartSessie(int id)
        {
            return RedirectToAction(nameof(Index), "Uitvoeren", new { geselecteerdeGroep = id });
        }
    }
}