using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Domain;

namespace TILE03.Models.ViewModels
{
    public class ActieViewModel
    {
        [Display(Name = "Toegangscode", Prompt = "Geef de toegangscode")]
        [Required(ErrorMessage = "Waarde moet ingevuld zijn!")]
        [RegularExpression(@"^[a-zA-Z]+[0-9]*")]
        public string Code { get; set; }

        public ActieViewModel()
        {
        }

        public ActieViewModel(String code) : this()
        {
            Code = code;
        }
    }
}