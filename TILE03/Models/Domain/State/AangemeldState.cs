using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TILE03.Models.Domain.State
{
    public class AangemeldState : GroepState
    {
        public AangemeldState(Groep groep) : base(groep)
        {
            
        }
        public override void Deblokkeer()
        {
            throw new NotImplementedException();
        }

        public override bool GeldigAntwoord(double value)
        {
            throw new NotImplementedException();
        }

        public override void SchatkistGevonden()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void VolgendeOpdracht()
        {
            throw new NotImplementedException();
        }
    }
}
