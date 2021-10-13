using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TILE03.Models.Exceptions
{
    public class StateException : Exception
    {
        public StateException() : base("Huidige state laat deze actie niet toe")
        {
        }

        public StateException(string message)
            : base(message)
        {
        }

        public StateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}