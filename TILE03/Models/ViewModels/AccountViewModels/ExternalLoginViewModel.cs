using System.ComponentModel.DataAnnotations;

namespace TILE03.Models.ViewModels.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}