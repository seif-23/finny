using System.ComponentModel.DataAnnotations;

namespace FinnyApp.Web.ViewModels
{
    public class UserSignUpViewModel
    {
        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(50, ErrorMessage = "الاسم لازم يكون أقل من 50 حرف")]
        public string Name { get; set; }

        [Required(ErrorMessage = "الإيميل مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة الإيميل غلط")]
        [StringLength(100, ErrorMessage = "الإيميل طويل أوي")]
        public string Email { get; set; }

        [Required(ErrorMessage = "الباسوورد مطلوب")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "الباسوورد لازم يكون 8 حروف على الأقل")]
        public string Password { get; set; }
    }
}