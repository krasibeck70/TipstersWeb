using System.ComponentModel.DataAnnotations;

namespace Tipsters.Models.ViewModels.AccountViewModel
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
