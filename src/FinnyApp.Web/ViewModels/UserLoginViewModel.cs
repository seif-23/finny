using System.ComponentModel.DataAnnotations;

namespace FinnyApp.Web.ViewModels
{
    public class UserLoginViewModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}