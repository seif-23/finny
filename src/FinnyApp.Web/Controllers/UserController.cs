using FinnyApp.DAL.Database;
using FinnyApp.DAL.Database.Entities;
using FinnyApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace FinnyApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _hasher;

        public UserController(DataContext context, IPasswordHasher<User> hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/login")]
        public IActionResult Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.UserTable.FirstOrDefault(u => u.Email == model.Email);

            if (user != null)
            {
                var result = _hasher.VerifyHashedPassword(user, user.Password, model.Password);
                if (result != PasswordVerificationResult.Failed)
                {
                    return Content("Login successful");
                }
            }

            ModelState.AddModelError("", "الإيميل أو الباسوورد غلط");
            return View(model);
        }

        [HttpGet("/signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("/signup")]
        public IActionResult SignUp(UserSignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.UserTable.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "الإيميل مستخدم بالفعل");
                return View(model);
            }

            var user = new User { Name = model.Name, Email = model.Email, Password = string.Empty };
            user.Password = _hasher.HashPassword(user, model.Password);

            _context.UserTable.Add(user);
            _context.SaveChanges();
            return Content("Sign up successful");
        }

        [HttpGet("/dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
