using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TILE03.Helper;
using TILE03.Models.Domain;
using TILE03.Models.Exceptions;
using TILE03.Models.Repositories;
using TILE03.Models.ViewModels;
using TILE03.Services;

namespace TILE03.Controllers
{
    //[ServiceFilter(typeof(GroepSessionFilter))]
    public class UitvoerenController : Controller
    {
        private readonly IGroepRepository _groepRepository;
        private readonly IOpdrachtRepository _opdrachtRepository;
        private readonly IOefeningRepository _oefeningRepository;
        private readonly IVooruitgangRepository _vooruitgangRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UitvoerenController(
            IGroepRepository groepRepository,
            IOpdrachtRepository opdrachtRepository,
            IOefeningRepository oefeningRepository,
            IVooruitgangRepository vooruitgangRepository,
            IHostingEnvironment hostingEnvironment)
        {
            _groepRepository = groepRepository;
            _opdrachtRepository = opdrachtRepository;
            _oefeningRepository = oefeningRepository;
            _vooruitgangRepository = vooruitgangRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        //De groep wordt in de juiste State gebracht en alle nodige ViewData wordt geset
        public IActionResult Index(int geselecteerdeGroep)
        {
            Groep groep = _groepRepository.GetById(geselecteerdeGroep);
            
            groep.Start();

            AddToContextAndPersist(groep);

            HttpContext.Session.SetString("groep", JsonConvert.SerializeObject(groep));

            SetPdf(groep);
            SetViewData(groep);

            return View(new AntwoordViewModel(new Antwoord()));
        }

        [HttpGet]
        [ServiceFilter(typeof(GroepSessionFilter))]
        //pdf tonen door SetPdf, deze zet de viewdata juist
        public IActionResult ToonPdf(Groep groep, AntwoordViewModel antwoordViewModel)
        {
            SetPdf(groep);
            SetViewData(groep);

            return View("Index", antwoordViewModel);
        }

        [ServiceFilter(typeof(GroepSessionFilter))]
        //de byte-array van de database wordt gelezen en in een opgave.pdf gestoken
        internal void SetPdf(Groep groep)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string resourcePath = "resources\\opgave.pdf";

            FileHelper.ConvertByteArrayToPDF(groep.HuidigeOpdracht.Oefening.OpgaveFile,
                _hostingEnvironment.WebRootPath + "\\" + resourcePath);

            ViewData["PDF"] = "../../resources/opgave.pdf";
        }

        [HttpGet]
        [ServiceFilter(typeof(GroepSessionFilter))]
        //SetPDF wordt niet aangeroepen waardoor deze null is en de pdf niet wordt getoond
        public IActionResult VerbergPdf(Groep groep, AntwoordViewModel antwoordViewModel)
        {
            SetViewData(groep);

            return View("Index", antwoordViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(GroepSessionFilter))]
        //Controle van het antwoord, indien juist wordt de actie view getoond
        public IActionResult ControleerAntwoord(Groep groep, AntwoordViewModel antwoordViewModel)
        {
            //Opdracht huidigeOpdracht = groep.HuidigeOpdracht;
            groep.PlaatsInReeks = groep.HuidigeOpdracht.VolgNr;
            groep.HuidigeOpdracht.Oefening = _oefeningRepository.GetById(groep.HuidigeOpdracht.Oefening.Id);

            try
            {
                if (groep.GeldigAntwoord(antwoordViewModel.Value))
                {
                    groep.Antwoord = antwoordViewModel.Value;

                    ViewData["Antwoord"] = groep.Antwoord;
                    ViewData["Actie"] = groep.HuidigeActie.Omschrijving;

                    _vooruitgangRepository.Update(groep.Vooruitgang);
                    AddToContextAndPersist(groep);
                    if (_opdrachtRepository.GetByVolgNr(groep.HuidigeOpdracht.VolgNr + 1) == null)
                    {
                        return View("SchatkistActie");
                    }

                    return View("Actie", new ActieViewModel());
                }

                SetPdf(groep);
                SetViewData(groep);

                ViewData["Fout"] = "Fout! Aantal resterende pogingen: " + (3 - groep.AantalVerkeerdeAntwoorden);
                return View("Index", antwoordViewModel);
            }
            catch (StateException e)
            {
                SetPdf(groep);
                SetViewData(groep);
                AddToContextAndPersist(groep);
                ViewData["Blocked"] = "Geblokkeerd, vraag hulp aan je leerkracht of lees de feedback en deblokkeer jezelf!";
                return View("Index", antwoordViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(GroepSessionFilter))]
        //hier wordt de toegangscode gecontroleerd en indien juist naar de volgende opdracht overgeschakeld
        public IActionResult VolgendeOpdracht(Groep groep, ActieViewModel actieViewModel)
        {
            int huidigeOpdrachtVolgNr = groep.HuidigeOpdracht.VolgNr;

            Opdracht volgendeOpdracht = _opdrachtRepository.GetByVolgNr(huidigeOpdrachtVolgNr + 1);

            String volgendeOpdrachtCode = volgendeOpdracht.Toegangscode.Code;

            if (actieViewModel.Code == volgendeOpdrachtCode)
            {
                groep.VolgendeOpdracht();
                SetPdf(groep);
                SetViewData(groep);

                return View("Index", new AntwoordViewModel(new Antwoord()));
            }
            else
            {
                ViewData["Antwoord"] = groep.Antwoord;
                ViewData["Actie"] = groep.HuidigeActie.Omschrijving;
                ViewData["Error"] = "Foute toegangscode!";
                return View("Actie", new ActieViewModel());
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(GroepSessionFilter))]
        public IActionResult SchatKistGevonden(Groep groep)
        {
            groep.SchatkistGevonden();
            AddToContextAndPersist(groep);
            return View("SchatkistGevonden");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(GroepSessionFilter))]
        public IActionResult Refresh(Groep groep, AntwoordViewModel antwoordViewModel)
        {
            if (_groepRepository.GetById(groep.Id).HuidigeState == "vergrendeld")
            {
                groep.Deblokkeer();
            }
            else
            {
                ViewData["Blocked"] = "Geblokkeerd, vraag hulp aan je leerkracht of lees de feedback en deblokkeer jezelf!";
            }
            SetViewData(groep);
            SetPdf(groep);

            return View(nameof(Index), antwoordViewModel);
        }

        [HttpGet]
        [ServiceFilter(typeof(GroepSessionFilter))]
        //groep deblokkeren
        public IActionResult Deblokkeer(Groep groep, AntwoordViewModel antwoordViewModel)
        {
            groep.Deblokkeer();
            AddToContextAndPersist(groep);
            SetViewData(groep);
            SetPdf(groep);
            return View(nameof(Index), antwoordViewModel);
        }

        internal void SetViewData(Groep groep)
        {
            Oefening oefening = _oefeningRepository.GetById(groep.HuidigeOpdracht.Oefening.Id);

            ViewData["OefeningOpgave"] = oefening.Naam;
            ViewData["GroepsBewerking"] = oefening.GroepsBewerking.ToString();
            ViewData["Vooruitgang"] = groep.Vooruitgang;
            ViewData["PlaatsInReeks"] = groep.PlaatsInReeks + " van de " + groep.TotaalAantalOefeningen + " opdrachten afgewerkt.";
            ViewData["Feedback"] = oefening.Feedback;
        }

        internal void AddToContextAndPersist(Groep g)
        {
            _groepRepository.Update(g);
            _groepRepository.SaveChanges();
        }
    }
}