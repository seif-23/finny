using FinnyApp.DAL.Database;
using FinnyApp.DAL.Database.Entities;
using FinnyApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.UserTable.Any(u => u.Email == model.Email && u.Password == model.Password))
                {
                    return Content("Login successful");
                }
            }
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

        [HttpGet("/dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
