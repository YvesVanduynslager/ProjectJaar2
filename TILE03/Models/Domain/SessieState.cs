using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TILE03.Models.Domain
{
    public abstract class SessieState
    {
        public Sessie Sessie { get; set; }
        public int Type { get; set; }

        protected SessieState(Sessie sessie)
        {
            Sessie = sessie;
        }

        public abstract void Activeer();
        public abstract void Deactiveer();
        public abstract void SpelMagStarten();
        public abstract void SpelMagNietStarten();
        public abstract void Uitvoeren();

    }
}