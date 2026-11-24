using FinnyApp.DAL.Database;
using FinnyApp.DAL.Database.Entities;
using FinnyApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
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

            var user = await _context.UserTable.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.ID.ToString()),
                    new(ClaimTypes.Email, user.Email)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Content("Login successful");
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
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                };
                _context.UserTable.Add(user);
                _context.SaveChanges();
                return Content("Sign up successful");
            }
            return View(model);
        }

        [Authorize]
        [HttpGet("/dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
