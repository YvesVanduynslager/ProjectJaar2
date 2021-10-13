using System.Collections;
using System.Collections.Generic;

namespace TILE03.Models.Domain
{
    public class UniekPad
    {
        #region Fields

        #endregion

        #region Properties

        public int Id { get; set; }
        public ICollection<Opdracht> Opdrachten { get; set; }
        public ICollection<Actie> Acties { get; set; }

        #endregion

        #region Constructors

        public UniekPad()
        {
            Opdrachten = new List<Opdracht>();
            Acties = new List<Actie>();
        }

        #endregion

        #region Methods

        #endregion

        //public Opdracht GeefVolgendeOpdracht(string toegangscode)
        //{
        //    IEnumerator OpdrachtenEnum = Opdrachten.GetEnumerator();
        //}
    }
}