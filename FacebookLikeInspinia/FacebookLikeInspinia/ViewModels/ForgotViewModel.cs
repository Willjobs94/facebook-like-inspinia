using System.ComponentModel.DataAnnotations;

namespace FacebookLikeInspinia.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}