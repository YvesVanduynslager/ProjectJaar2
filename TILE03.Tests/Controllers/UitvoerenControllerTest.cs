using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TILE03.Controllers;
using TILE03.Helper;
using TILE03.Models.Domain;
using TILE03.Models.Domain.BewerkingStrategy;
using TILE03.Models.Exceptions;
using TILE03.Models.Repositories;
using TILE03.Models.ViewModels;
using Xunit;

namespace TILE03.Tests.Controllers
{
    public class UitvoerenControllerTest
    {
        private readonly UitvoerenController _uitvoerenController;
        private readonly Mock<IGroepRepository> _groepRepository;
        private readonly Mock<IOpdrachtRepository> _opdrachtRepository;
        private readonly Mock<IOefeningRepository> _oefeningRepository;
        private readonly Mock<IVooruitgangRepository> _vooruitgangRepository;
        private readonly Mock<IHostingEnvironment> _hostingEnv;
        private Groep _dummyGroep;

        public UitvoerenControllerTest()
        {
            _groepRepository = new Mock<IGroepRepository>();
            _opdrachtRepository = new Mock<IOpdrachtRepository>();
            _oefeningRepository = new Mock<IOefeningRepository>();
            _vooruitgangRepository = new Mock<IVooruitgangRepository>();
            _hostingEnv = new Mock<IHostingEnvironment>();
            _uitvoerenController = new UitvoerenController(_groepRepository.Object, _opdrachtRepository.Object,
                _oefeningRepository.Object, _vooruitgangRepository.Object, _hostingEnv.Object);

            _dummyGroep = new Groep();
            SetupGroepen();
        }

        [Fact(Skip="Opslaan van groep in sessie geeft hier problemen")]
        public void Index_PassesEmptyAntwoordViewModelinViewResultModel()
        {
            //Arrange
            _groepRepository.Setup(g => g.GetById(It.IsAny<int>())).Returns(_dummyGroep);
            _opdrachtRepository.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns(_dummyGroep.UniekePad.Opdrachten.FirstOrDefault());
            _oefeningRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(_dummyGroep.HuidigeOpdracht?.Oefening);

            //Act
            var result = _uitvoerenController.Index(/*_dummyGroep, */1) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [Fact]
        public void ControleerAntwoord_GeenVolgendeOpdrachtReturnsSchatKistGevondenView()
        {
            //Arrange
            _dummyGroep.Start();

            _groepRepository.Setup(g => g.GetById(It.IsAny<int>())).Returns(_dummyGroep);
            _opdrachtRepository.Setup(m => m.GetByVolgNr(It.IsAny<int>())).Returns((Opdracht) null);
            _oefeningRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(_dummyGroep.HuidigeOpdracht?.Oefening);

            AntwoordViewModel avm = new AntwoordViewModel(new Antwoord());
            avm.Value = 5;

            //Act
            var result = _uitvoerenController.ControleerAntwoord(_dummyGroep, avm) as ViewResult;
            //Assert
            Assert.NotNull(result);
            Assert.True( /*string.IsNullOrEmpty(result.ViewName) || */result.ViewName == "SchatkistActie");
        }

        [Fact]
        public void ControleerAntwoord_AntwoordCorrectReturnsActieView()
        {
            //Arrange
            _dummyGroep.Start();

            _groepRepository.Setup(g => g.GetById(It.IsAny<int>())).Returns(_dummyGroep);
            _opdrachtRepository.Setup(m => m.GetByVolgNr(It.IsAny<int>())).Returns(new Opdracht());
            _oefeningRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(_dummyGroep.HuidigeOpdracht?.Oefening);

            AntwoordViewModel avm = new AntwoordViewModel(new Antwoord());
            avm.Value = 5;

            //Act
            var result = _uitvoerenController.ControleerAntwoord(_dummyGroep, avm) as ViewResult;
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.True( /*string.IsNullOrEmpty(result.ViewName) || */result.ViewName == "Actie");
        }

        [Fact(Skip="place setupPDF in separate class")]
        public void ControleerAntwoord_FoutAntwoordReturnsIndexView()
        {
            //Arrange
            _dummyGroep.Start();

            _groepRepository.Setup(g => g.GetById(It.IsAny<int>())).Returns(_dummyGroep);
            _opdrachtRepository.Setup(m => m.GetByVolgNr(It.IsAny<int>())).Returns(new Opdracht());
            _oefeningRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(_dummyGroep.HuidigeOpdracht?.Oefening);

            AntwoordViewModel avm = new AntwoordViewModel(new Antwoord());
            avm.Value = 3;

            //Act
            var result = _uitvoerenController.ControleerAntwoord(_dummyGroep, avm) as ViewResult;
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.True( /*string.IsNullOrEmpty(result.ViewName) || */result.ViewName == "Index");
        }

        [Fact(Skip="place setupPDF in separate class")]
        public void ControleerAntwoord_GeblokkeerdReturnsIndexView()
        {
            //Arrange
            _dummyGroep.Start();

            _groepRepository.Setup(g => g.GetById(It.IsAny<int>())).Returns(_dummyGroep);
            _opdrachtRepository.Setup(m => m.GetByVolgNr(It.IsAny<int>())).Returns(new Opdracht());
            _oefeningRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(_dummyGroep.HuidigeOpdracht?.Oefening);

            AntwoordViewModel avm = new AntwoordViewModel(new Antwoord());
            avm.Value = 5;

            _dummyGroep.ToState("geblokkeerd");
            //Act
            var result = _uitvoerenController.ControleerAntwoord(_dummyGroep, avm) as ViewResult;
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.True( /*string.IsNullOrEmpty(result.ViewName) || */result.ViewName == "Index");
        }

        [Fact]
        public void GeldigAntwoord_Foutief_ThrowsStateException()
        {
            //Arrange
            _dummyGroep.Start();

            _groepRepository.Setup(g => g.GetById(It.IsAny<int>())).Returns(_dummyGroep);
            _opdrachtRepository.Setup(m => m.GetByVolgNr(It.IsAny<int>())).Returns(new Opdracht());
            _oefeningRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(_dummyGroep.HuidigeOpdracht?.Oefening);

            AntwoordViewModel avm = new AntwoordViewModel(new Antwoord());
            avm.Value = 5;

            _dummyGroep.ToState("geblokkeerd");
            //Act
            Assert.Throws<StateException>(() => _dummyGroep.GroepState.GeldigAntwoord(3));
        }


        private void SetupGroepen()
        {
            Leerling l1 = new Leerling {Naam = "Vanduynslager", Voornam = "Yves"};
            Leerling l2 = new Leerling {Naam = "Vanrotsbakker", Voornam = "Ivo"};
            Leerling l3 = new Leerling {Naam = "Bert", Voornam = "Sesam"};
            Leerling l4 = new Leerling {Naam = "Ernie", Voornam = "Sesam"};

            _dummyGroep = new Groep();
            _dummyGroep.Leerlingen.Add(l1);
            _dummyGroep.Leerlingen.Add(l2);

            Oefening oppervlakte = new Oefening
            {
                GroepsBewerking = new Aftrekken(5),
                Naam = "Oppervlakte",
                //OpgaveFile = FileHelper.ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Oppervlakte.pdf"),
                Antwoord = new Antwoord(10)
            };
            Oefening driehoeken = new Oefening
            {
                GroepsBewerking = new Vermenigvuldig(3),
                Naam = "Driehoeken",
                //OpgaveFile = FileHelper.ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Driehoeken.pdf"),
                Antwoord = new Antwoord(5)
            };
            Oefening vergelijkingen = new Oefening
            {
                GroepsBewerking = new Delen(2),
                Naam = "Vergelijkingen",
                //OpgaveFile = FileHelper.ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Vergelijkingen.pdf"),
                Antwoord = new Antwoord(1)
            };
            Oefening domino = new Oefening
            {
                GroepsBewerking = new Optellen(11),
                Naam = "Domino",
                //OpgaveFile = FileHelper.ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Domino.pdf"),
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
            _dummyGroep.UniekePad = padVoorEenGroep;
            Sessie sessie1 = new Sessie
            {
                Code = "S001",
                Naam = "Sessie 1",
                Omschrijving = "Een eerste sessie in .NET",
                StateType = 0
            };
            sessie1.Groepen.Add(_dummyGroep);
            Klas klas = new Klas();
            klas.Leerlingen.Add(l1);
            klas.Leerlingen.Add(l2);
            klas.Leerlingen.Add(l3);
            klas.Leerlingen.Add(l4);
            sessie1.Klas = klas;
        }
    }
}