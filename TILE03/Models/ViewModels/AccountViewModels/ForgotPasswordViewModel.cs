using System.ComponentModel.DataAnnotations;

namespace TILE03.Models.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}