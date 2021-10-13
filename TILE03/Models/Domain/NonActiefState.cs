using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TILE03.Models.Domain
{
    public class NonActiefState : SessieState
    {
        public NonActiefState(Sessie sessie) : base(sessie)
        {
            Type = 0;
        }

        public override void Activeer()
        {
            Sessie.ToState(Sessie.ActiefState);
        }

        public override void Deactiveer()
        {
            throw new NotImplementedException();
        }

        public override void SpelMagNietStarten()
        {
            throw new NotImplementedException();
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