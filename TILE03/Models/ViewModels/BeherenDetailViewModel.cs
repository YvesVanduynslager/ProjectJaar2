using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Domain;

namespace TILE03.Models.ViewModels
{
    //wordt niet gebruikt
    public class BeherenDetailViewModel
    {
        public string Code { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public bool Actief { get; set; }

        public BeherenDetailViewModel(Sessie sessie)
        {
            
        }
    }
}
