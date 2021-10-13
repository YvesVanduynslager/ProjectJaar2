using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TILE03.Models.ViewModels
{
    public class DeelnemenIndexViewModel
    {
        [Display(Name = "Sessiecode", Prompt = "Sessiecode")]
        [Required(ErrorMessage = "Waarde moet ingevuld zijn!")]
        [RegularExpression(@"^[a-zA-Z]+[0-9]*")]
        public string Code { get; set; }

        public DeelnemenIndexViewModel()
        {
        }

        public DeelnemenIndexViewModel(String code) : this()
        {
            Code = code;
        }
    }
}