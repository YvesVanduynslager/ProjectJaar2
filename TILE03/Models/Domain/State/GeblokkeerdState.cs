using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Exceptions;

namespace TILE03.Models.Domain.State
{
    public class GeblokkeerdState : GroepState
    {
        public GeblokkeerdState(Groep groep) : base(groep)
        {
            
        }

        public override void Deblokkeer()
        {
            Groep.AantalVerkeerdeAntwoorden = 0;
            Groep.ToState("vergrendeld");
        }

        public override bool GeldigAntwoord(double value)
        {
            throw new StateException();
        }

        public override void Start()
        {
            throw new StateException();
        }

        public override void VolgendeOpdracht()
        {
            throw new StateException();
        }

        public override void SchatkistGevonden()
        {
            throw new StateException();
        }
    }
}