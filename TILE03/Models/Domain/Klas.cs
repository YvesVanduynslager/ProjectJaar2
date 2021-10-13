using System;
using System.Collections;
using System.Collections.Generic;

namespace TILE03.Models.Domain
{
    public class Klas
    {
        #region Fields

        #endregion

        #region Properties

        public int Id { get; set; }
        public string Code { get; set; }
        public ICollection<Leerling> Leerlingen { get; set; }

        #endregion

        #region Constructors

        public Klas()
        {
            Leerlingen = new HashSet<Leerling>();
        }

        #endregion

        #region Methods

        #endregion
    }
}