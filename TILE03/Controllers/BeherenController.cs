using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TILE03.Models.Domain;
using TILE03.Models.Repositories;
using TILE03.Services;

namespace TILE03.Controllers
{
    public class BeherenController : Controller
    {
        private readonly ISessieRepository _sessieRepository;
        private readonly IGroepRepository _groepRepository;

        public BeherenController(ISessieRepository sessieRepository, IGroepRepository groepRepository)
        {
            _sessieRepository = sessieRepository;
            _groepRepository = groepRepository;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        //overzicht van alle sessies
        public IActionResult Index()
        {
            IEnumerable<Sessie> sessies = _sessieRepository.GetAll();
            return View(sessies);
        }

        [Authorize(Policy="AdminOnly")]
        [HttpGet]
        //details van de sessie
        public IActionResult ToonGroepen(int id)
        {
            Sessie sessie = _sessieRepository.GetById(id);
            return View(sessie);
        }

        [Authorize(Policy="AdminOnly")]
        [HttpGet]
        //Deblokkeren van een groep in een sessie, vergrendeld state is dat de groep het spel aan het spelen is
        public IActionResult Deblokkeer(int id, int sessieId)
        {
            Groep groep = _groepRepository.GetById(id);
            if (groep.HuidigeState == "geblokkeerd")
            {
                groep.HuidigeState = "vergrendeld";
            }
            _groepRepository.SaveChanges();
            return RedirectToAction(nameof(ToonGroepen), new { id = sessieId });
        }

        //[Authorize(Policy = "AdminOnly")]
        //[HttpGet]
        //public IActionResult Blokkeer(int id)
        //{
        //    Groep groep = _groepRepository.GetById(id);
        //    if (groep.HuidigeState == "vergrendeld")
        //    {
        //        groep.HuidigeState = "geblokkeerd";
        //    }
        //    _groepRepository.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        //De sessie activeren
        public IActionResult Activeer(int id)
        {
            Sessie sessie = _sessieRepository.GetById(id);
            sessie.ToState(sessie.ActiefState);
            _sessieRepository.SaveChanges();
            return RedirectToAction(nameof(ToonGroepen), new { id = id});
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        //De sessie deactiveren, hier veranderen we ook de groepStates terug naar 'niet aangemeld'
        public IActionResult Deactiveer(int id)
        {
            Sessie sessie = _sessieRepository.GetById(id);
            foreach (var groep in sessie.Groepen)
            {
                groep.ToState("Niet Aangemeld");
            }
            _groepRepository.SaveChanges();
            sessie.ToState(sessie.NonActiefState);
            _sessieRepository.SaveChanges();
            return RedirectToAction(nameof(ToonGroepen), new { id = id });
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        //De sessie starten: dit is de leerkracht die het signaal heeft dat er nu mag gestart worden met het spel te spelen
        public IActionResult StartSpel(int id)
        {
            Sessie sessie = _sessieRepository.GetById(id);
            sessie.ToState(sessie.StartSpelState);
            _sessieRepository.SaveChanges();
            return View("ToonGroepen", sessie);
        }
    }
}