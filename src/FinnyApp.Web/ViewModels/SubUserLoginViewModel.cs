using System.ComponentModel.DataAnnotations;

namespace FinnyApp.Web.ViewModels
{
    public class SubUserLoginViewModel
    {
        [Required(ErrorMessage = "إيميل ولي الأمر مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة الإيميل غلط")]
        public string Email { get; set; }

        [Required(ErrorMessage = "الـ PIN مطلوب")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "الـ PIN لازم يكون 4 أرقام")]
        public string PIN { get; set; }
    }
}