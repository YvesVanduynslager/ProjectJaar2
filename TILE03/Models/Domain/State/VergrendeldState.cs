using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Exceptions;
using TILE03.Models.Repositories;

namespace TILE03.Models.Domain.State
{
    public class VergrendeldState : GroepState
    {
        public VergrendeldState(Groep groep) : base(groep)
        {
            
        }

        public override void Deblokkeer()
        {
            throw new StateException();
        }

        public override bool GeldigAntwoord(double value)
        {
            if (Groep.HuidigeOpdracht.Oefening.CorrectAntwoord(value))
            {
                int volgNrHuidigeOpdracht = Groep.HuidigeOpdracht.VolgNr;
                string naamHuidigeOefening = Groep.HuidigeOpdracht.Oefening.Naam;

                Groep.Vooruitgang.Add(new Vooruitgang(volgNrHuidigeOpdracht, naamHuidigeOefening, value));

                Groep.PlaatsInReeks = volgNrHuidigeOpdracht;
                Groep.AantalVerkeerdeAntwoorden = 0;
                return true;
            }

            Groep.AantalVerkeerdeAntwoorden++;
            if (Groep.AantalVerkeerdeAntwoorden >= 2)
            {
                Groep.ToState("geblokkeerd");
            }

            return false;
        }

        public override void Start()
        {
            Groep.Vooruitgang = new List<Vooruitgang>();
            Groep.HuidigeOpdracht = Groep.UniekePad.Opdrachten.SingleOrDefault(o => o.VolgNr == 1);
            Groep.PlaatsInReeks = 0;
            Groep.TotaalAantalOefeningen = Groep.UniekePad.Opdrachten.Count;
            VolgendeActie();
        }

        private void VolgendeActie()
        {
            foreach (var actie in Groep.UniekePad.Acties.OrderBy(o => o.Id))
            {
                if (!actie.Gebruikt)
                {
                    Groep.HuidigeActie = actie;
                    break;
                }
            }
            //Groep.HuidigeActie = Groep.UniekePad.Acties.FirstOrDefault();

            //Groep.UniekePad.Acties.Remove(Groep.HuidigeActie);
        }

        public override void VolgendeOpdracht()
        {
            Actie actie = Groep.UniekePad.Acties.First(a => !a.Gebruikt);
            actie.Gebruikt = true;

            int volgNrHuidigeOpdracht = Groep.HuidigeOpdracht.VolgNr;

            Groep.HuidigeOpdracht =
                Groep.UniekePad.Opdrachten.SingleOrDefault(opdr => opdr.VolgNr == (volgNrHuidigeOpdracht + 1));

            Groep.AantalVerkeerdeAntwoorden = 0;

            VolgendeActie();
        }

        public override void SchatkistGevonden()
        {
            Groep.ToState("schatkistgevonden");
        }
    }
}