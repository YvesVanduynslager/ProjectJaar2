using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TILE03.Models.Domain.State
{
    public static class StateFactory
    {
        //dit is voor aanmaak van de correcte State-objecten, voor groepState is dit met een string
        public static GroepState Get(Groep groep, string state)
        {
            switch (state)
            {
                case "geblokkeerd": return new GeblokkeerdState(groep);
                case "ontgrendeld": return new OntgrendeldState(groep);
                case "vergrendeld": return new VergrendeldState(groep);
                case "schatkistgevonden": return new SchatGevondenState(groep);
                case "Niet Aangemeld": return new NietAangemeldState(groep);
                case "Aangemeld": return new AangemeldState(groep);
                default: return null;
            }
        }
        //via SessieState wordt gewerkt met een integer
        public static SessieState GetSessieState(Sessie sessie, int stateType)
        {
            switch (stateType)
            {
                case 0: return new NonActiefState(sessie);
                case 1: return new ActiefState(sessie);
                case 2: return new StartSpelState(sessie);
                default:
                    return null;
            }
        }
    }
}