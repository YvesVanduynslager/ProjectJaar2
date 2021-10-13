using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TILE03.Models.Domain
{
    public class Actie
    {
        #region Fields

        #endregion

        #region Properties

        public int Id { get; set; }
        public string Omschrijving { get; set; }
        [NotMapped] public bool Gebruikt { get; set; }// = false;

        #endregion

        #region Constructors

        public Actie()
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}