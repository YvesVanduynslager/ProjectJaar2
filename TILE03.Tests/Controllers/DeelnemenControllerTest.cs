using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TILE03.Controllers;
using TILE03.Models.Domain;
using TILE03.Models.Domain.BewerkingStrategy;
using TILE03.Models.Repositories;
using TILE03.Models.ViewModels;
using Xunit;
using Xunit.Sdk;

namespace TILE03.Tests.Controllers
{
    public class DeelnemenControllerTest
    {
        private readonly DeelnemenController _deelnemenController;
        private readonly Mock<ISessieRepository> _sessieRepository;
        private readonly Mock<IGroepRepository> _groepRepository;
        private Sessie _dummySessie;
        private Groep _dummyGroep;
        public DeelnemenControllerTest()
        {
            _sessieRepository = new Mock<ISessieRepository>();
            _groepRepository = new Mock<IGroepRepository>();
            _deelnemenController = new DeelnemenController(_sessieRepository.Object, _groepRepository.Object);

            _dummySessie = new Sessie();
            _dummyGroep = new Groep();
            Setup();
        }

        [Fact]
        public void Index_ReturnsIndexViewWithDeelnemenIndexViewModel()
        {
            //Act
            var result = _deelnemenController.Index() as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<DeelnemenIndexViewModel>(result.Model);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [Fact]
        public void ControleerSessieCode_SessieNietActief_ReturnsIndexView()
        {
            //Arrange
            _sessieRepository.Setup(s => s.GetByCode(It.IsAny<string>())).Returns(_dummySessie);

            //Act
            var result = _deelnemenController.ControleerSessiecode(new DeelnemenIndexViewModel()) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.ViewName) || result.ViewName =="Index");
        }

        [Fact]
        public void ControleerSessieCode_SessieNietGevonden_ReturnsIndexView()
        {
            //Arrange
            _sessieRepository.Setup(s => s.GetByCode(It.IsAny<string>())).Returns((Sessie)null);

            //Act
            var result = _deelnemenController.ControleerSessiecode(new DeelnemenIndexViewModel()) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.ViewName) || result.ViewName =="Index");
        }

        [Fact]
        public void ControleerSessieCode_CorrecteCode_RedirectsToOpenSessie()
        {
            //Arrange
            _dummySessie.StateType = 1;
            _sessieRepository.Setup(s => s.GetByCode(It.IsAny<string>())).Returns(_dummySessie);

            //Act
            var result =
                _deelnemenController.ControleerSessiecode(new DeelnemenIndexViewModel()) as RedirectToActionResult;

            //Assert
            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.ActionName) && !result.RouteValues.IsNullOrEmpty());
            Assert.Equal("OpenSessie",result.ActionName);
        }

        [Fact]
        public void OpenSessie_ReturnsOpenSessieView()
        {
            //Arrange
            _sessieRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(_dummySessie);

            //Act
            var result = _deelnemenController.OpenSessie(1) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName =="OpenSessie");
        }

        [Fact]
        public void StartSessie_RedirectsToUitvoerenIndexAction()
        {
            //Act
            var result = _deelnemenController.StartSessie(1) as RedirectToActionResult;

            //Assert
            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.ActionName) && !string.IsNullOrEmpty(result.ControllerName) && !result.RouteValues.IsNullOrEmpty());
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Uitvoeren", result.ControllerName);
        }

        [Fact]
        public void GroepAanmelden_RedirectsToDeelnemenInWachtAction()
        {
            //Arrange
            _groepRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(_dummyGroep);

            //Act
            var result = _deelnemenController.GroepAanmelden(1, 1) as RedirectToActionResult;

            //Assert
            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.ActionName) && !string.IsNullOrEmpty(result.ControllerName) && !result.RouteValues.IsNullOrEmpty());
            Assert.Equal("InWacht", result.ActionName);
            Assert.Equal("Deelnemen", result.ControllerName);
        }

        [Fact(Skip="Response.Headers.Add geeft problemen bij testen")]
        public void InWacht_ReturnsInWachtView()
        {
            //Arrange
            _sessieRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(_dummySessie);

            //Act
            var result = _deelnemenController.InWacht(1, 1) as ViewResult;

                //Assert
            Assert.NotNull(result);
            Assert.IsType<Sessie>(result.Model);
            Assert.True(!string.IsNullOrEmpty(result.ViewName) || result.ViewName == "InWacht");
        }

        private void Setup()
        {
            Leerling l1 = new Leerling {Naam = "Vanduynslager", Voornam = "Yves"};
            Leerling l2 = new Leerling {Naam = "Vanrotsbakker", Voornam = "Ivo"};
            Leerling l3 = new Leerling {Naam = "Bert", Voornam = "Sesam"};
            Leerling l4 = new Leerling {Naam = "Ernie", Voornam = "Sesam"};

            Groep groep = new Groep();
            Groep _dummyGroep = new Groep();
            groep.Leerlingen.Add(l1);
            groep.Leerlingen.Add(l2);
            _dummyGroep.Leerlingen.Add(l1);
            _dummyGroep.Leerlingen.Add(l2);

            Oefening oppervlakte = new Oefening
            {
                GroepsBewerking = new Aftrekken(5),
                Naam = "Oppervlakte",
                //OpgaveFile = ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Oppervlakte.pdf"),
                Antwoord = new Antwoord(10)
            };
            Oefening driehoeken = new Oefening
            {
                GroepsBewerking = new Vermenigvuldig(3),
                Naam = "Driehoeken",
                //OpgaveFile = ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Driehoeken.pdf"),
                Antwoord = new Antwoord(5)
            };
            Oefening vergelijkingen = new Oefening
            {
                GroepsBewerking = new Delen(2),
                Naam = "Vergelijkingen",
                //OpgaveFile = ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Vergelijkingen.pdf"),
                Antwoord = new Antwoord(1)
            };
            Oefening domino = new Oefening
            {
                GroepsBewerking = new Optellen(11),
                Naam = "Domino",
                //OpgaveFile = ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Domino.pdf"),
                Antwoord = new Antwoord(13)
            };

            Opdracht opdracht1 =
                new Opdracht {VolgNr = 1, Oefening = oppervlakte, Toegangscode = new Toegangscode {Code = "A001"}};
            Opdracht opdracht2 =
                new Opdracht {VolgNr = 3, Oefening = driehoeken, Toegangscode = new Toegangscode {Code = "A002"}};
            Opdracht opdracht3 =
                new Opdracht {VolgNr = 2, Oefening = vergelijkingen, Toegangscode = new Toegangscode {Code = "A003"}};
            Opdracht opdracht4 =
                new Opdracht {VolgNr = 4, Oefening = domino, Toegangscode = new Toegangscode {Code = "A004"}};

            Actie a1 = new Actie
            {
                Omschrijving =
                    "Verspreid in de klas liggen enveloppen verstopt.Zoek de enveloppe waarop het resultaat staat.In die enveloppe vind je de volgende opdracht"
            };
            Actie a2 = new Actie
            {
                Omschrijving =
                    "In de klas zijn glazen potten verstopt, zoek de glazen pot waarop het resultaat geschreven staat. In die pot vind je de volgende opdracht"
            };
            Actie a3 = new Actie
            {
                Omschrijving =
                    "In de klas liggen wc-rolletjes verstopt. Zoek het wc-rolletje waarop het resultaat, daarin vinden jullie de nieuwe opdracht"
            };
            Actie a4 = new Actie
            {
                Omschrijving =
                    "Zoeken naar schatkist"
            };

            ICollection<Opdracht> opdrachten = new List<Opdracht>();
            opdrachten.Add(opdracht1);
            opdrachten.Add(opdracht2);
            opdrachten.Add(opdracht3);
            opdrachten.Add(opdracht4);
            ICollection<Actie> acties = new List<Actie>();
            acties.Add(a1);
            acties.Add(a2);
            acties.Add(a3);
            acties.Add(a4);

            UniekPad padVoorEenGroep = new UniekPad {Acties = acties, Opdrachten = opdrachten};
            groep.UniekePad = padVoorEenGroep;
            _dummySessie = new Sessie
            {
                Code = "S001",
                Naam = "Sessie 1",
                Omschrijving = "Een eerste sessie in .NET",
                StateType = 0
            };
            _dummySessie.Groepen.Add(groep);
            Klas klas = new Klas();
            klas.Leerlingen.Add(l1);
            klas.Leerlingen.Add(l2);
            klas.Leerlingen.Add(l3);
            klas.Leerlingen.Add(l4);
            _dummySessie.Klas = klas;
        }
    }
}
