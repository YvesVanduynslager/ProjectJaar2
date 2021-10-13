using System.ComponentModel.DataAnnotations;

namespace TILE03.Models.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het wachtwoord moet minstens {2} en maximum {1} tekens bevatten.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name="Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name="Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}