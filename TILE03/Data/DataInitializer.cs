using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TILE03.Helper;
using TILE03.Models;
using TILE03.Models.Domain;
using TILE03.Models.Domain.BewerkingStrategy;

namespace TILE03.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                await InitializeUsers();

                Leerling l1 = new Leerling {Naam = "Vanduynslager", Voornam = "Yves"};
                Leerling l2 = new Leerling {Naam = "De Jaeger", Voornam = "David"};
                Leerling l3 = new Leerling {Naam = "Bert", Voornam = "Sesam"};
                Leerling l4 = new Leerling {Naam = "Ernie", Voornam = "Sesam"};

                Groep groep1 = new Groep();
                groep1.Leerlingen.Add(l1);
                groep1.Leerlingen.Add(l2);

                Groep groep2 = new Groep();
                groep2.Leerlingen.Add(l3);
                groep2.Leerlingen.Add(l4);

                Oefening oppervlakte = new Oefening
                {
                    GroepsBewerking = new Aftrekken(5),
                    Naam = "Oppervlakte",
                    OpgaveFile = FileHelper.ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Oppervlakte.pdf"),
                    Feedback = "Vergeet je groepsbewerking niet te doen vooraleer je het antwoord ingeeft!",
                    Antwoord = new Antwoord(10)
                };
                Oefening driehoeken = new Oefening
                {
                    GroepsBewerking = new Vermenigvuldig(3),
                    Naam = "Driehoeken",
                    OpgaveFile = FileHelper.ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Driehoeken.pdf"),
                    Feedback = "Vergeet je groepsbewerking niet te doen vooraleer je het antwoord ingeeft!",
                    Antwoord = new Antwoord(5)
                };
                Oefening vergelijkingen = new Oefening
                {
                    GroepsBewerking = new Delen(2),
                    Naam = "Vergelijkingen",
                    OpgaveFile = FileHelper.ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Vergelijkingen.pdf"),
                    Feedback = "Vergeet je groepsbewerking niet te doen vooraleer je het antwoord ingeeft!",
                    Antwoord = new Antwoord(1)
                };
                Oefening domino = new Oefening
                {
                    GroepsBewerking = new Optellen(11),
                    Naam = "Domino",
                    OpgaveFile = FileHelper.ConvertPDFtoByteArray("Data\\DataInitializerPDFs\\Domino.pdf"),
                    Feedback = "Vergeet je groepsbewerking niet te doen vooraleer je het antwoord ingeeft!",
                    Antwoord = new Antwoord(13)
                };

                Opdracht opdracht1 =
                    new Opdracht {VolgNr = 1, Oefening = oppervlakte, Toegangscode = new Toegangscode {Code = "A001"}};
                Opdracht opdracht2 =
                    new Opdracht {VolgNr = 3, Oefening = driehoeken, Toegangscode = new Toegangscode {Code = "A002"}};
                Opdracht opdracht3 =
                    new Opdracht
                    {
                        VolgNr = 2,
                        Oefening = vergelijkingen,
                        Toegangscode = new Toegangscode {Code = "A003"}
                    };
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
                groep1.UniekePad = padVoorEenGroep;
                Sessie sessie1 = new Sessie
                {
                    Code = "S001",
                    Naam = "Sessie 1",
                    Omschrijving = "Een eerste sessie in .NET",
                    StateType = 0
                };
                Sessie sessie2 = new Sessie
                {
                    Code = "S002",
                    Naam = "Sessie 2",
                    Omschrijving = "Een tweede sessie in .NET",
                    StateType = 0
                };
                sessie1.Groepen.Add(groep1);
                sessie1.Groepen.Add(groep2);
                Klas klas = new Klas();
                klas.Leerlingen.Add(l1);
                klas.Leerlingen.Add(l2);
                klas.Leerlingen.Add(l3);
                klas.Leerlingen.Add(l4);
                sessie1.Klas = klas;

                _dbContext.Sessies.Add(sessie1);
                _dbContext.Sessies.Add(sessie2);
                _dbContext.SaveChanges();
            }
        }

        public async Task InitializeUsers()
        {
            string email = "leerkracht@hogent.be";
            ApplicationUser user = new ApplicationUser {UserName = email, Email = email};
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
        }
    }
}