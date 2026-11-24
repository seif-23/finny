using System.ComponentModel.DataAnnotations;

namespace FinnyApp.Web.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "الإيميل مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة الإيميل غلط")]
        public string Email { get; set; }

        [Required(ErrorMessage = "الباسوورد مطلوب")]
        public string Password { get; set; }
    }
}