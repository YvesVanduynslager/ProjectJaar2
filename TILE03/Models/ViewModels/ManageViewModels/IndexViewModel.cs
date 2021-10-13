using System.ComponentModel.DataAnnotations;

namespace TILE03.Models.ViewModels.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name="Gebruiker")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Phone]
        [Display(Name = "Telefoon")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}