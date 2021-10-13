using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TILE03.Models.Domain
{
    public class ActiefState : SessieState
    {
        public ActiefState(Sessie sessie) : base(sessie)
        {
            Type = 1;
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
            throw new NotImplementedException();
        }

        public override void SpelMagStarten()
        {
            Sessie.ToState(Sessie.StartSpelState);
        }

        public override void Uitvoeren()
        {
            throw new NotImplementedException();
        }

    }
}