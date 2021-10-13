using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TILE03.Models.Domain.State
{
    [NotMapped]
    public abstract class GroepState
    {
        protected Groep Groep { get; set; }

        protected GroepState(Groep groep)
        {
            Groep = groep;
        }

        public abstract void Start();
        public abstract void VolgendeOpdracht();
        public abstract bool GeldigAntwoord(double value);

        public abstract void Deblokkeer();
        public abstract void SchatkistGevonden();
    }
}