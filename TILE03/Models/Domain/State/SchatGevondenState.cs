using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Exceptions;

namespace TILE03.Models.Domain.State
{
    public class SchatGevondenState : GroepState
    {
        public SchatGevondenState(Groep groep) : base(groep)
        {
            
        }

        public override void Deblokkeer()
        {
            throw new StateException();
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