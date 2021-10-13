using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TILE03.Models.Domain
{
    public class StartSpelState : SessieState
    {
        public StartSpelState(Sessie sessie) : base(sessie)
        {
            Type = 2;   
        }
        public override void Activeer()
        {
            throw new NotImplementedException();
        }

        public override void Deactiveer()
        {
            Sessie.ToState(Sessie.NonActiefState);
        }

        public override void SpelMagNietStarten()
        {
            Sessie.ToState(Sessie.ActiefState);
        }

        public override void SpelMagStarten()
        {
            throw new NotImplementedException();
        }

        public override void Uitvoeren()
        {
            throw new NotImplementedException();
        }
    }
}
