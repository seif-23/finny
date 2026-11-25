using FinnyApp.DAL.Database;
using FinnyApp.DAL.Database.Entities;
using FinnyApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;

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
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.UserTable.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null)
            {
                var result = _hasher.VerifyHashedPassword(user, user.Password, model.Password);
                if (result != PasswordVerificationResult.Failed)
                {
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, user.ID.ToString()),
                        new(ClaimTypes.Email, user.Email)
                    };

                    var identity = new ClaimsIdentity(claims, "FinnyUserCookie");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("FinnyUserCookie", principal);

                    return Content("Login successful");
                }
            }
            ModelState.AddModelError(string.Empty, "No User Found!");
            return View();
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

        [Authorize(AuthenticationSchemes = "FinnyUserCookie")]
        [HttpGet("/dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "FinnyUserCookie")]
        [HttpPost("/dashboard")]
        public async Task<IActionResult> Dashboard(CreateSubUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = await HttpContext.AuthenticateAsync("FinnyUserCookie");

                if (userId.Succeeded)
                {
                    var ID = userId.Principal?.FindFirst(ClaimTypes.Name)?.Value;
                    var user = _context.UserTable.FirstOrDefault(u => u.ID.ToString() == ID);
                    var subUser = new SubUser
                    {
                        User = user,
                        Name = model.Name,
                        PIN = model.PIN,
                    };
                    _context.SubUserTable.Add(subUser);
                    _context.SaveChanges();
                    return Content("SubUser created successfully");
                }
            }
            return View(model);
        }
    }
}
