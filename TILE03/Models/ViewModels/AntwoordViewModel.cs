using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Domain;

namespace TILE03.Models.ViewModels
{
    public class AntwoordViewModel
    {
        [Display(Name = "Antwoord")]
        [Required(ErrorMessage = "Waarde moet ingevuld zijn!")]
        [RegularExpression(@"-?\d+(?:\,\d+)?")]
        public double Value { get; set; }

        public AntwoordViewModel()
        {
        }

        public AntwoordViewModel(Antwoord antwoord) : this()
        {
            Value = antwoord.Value;
        }
    }
}