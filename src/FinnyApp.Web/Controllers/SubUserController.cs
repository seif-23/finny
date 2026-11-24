using FinnyApp.DAL.Database;
using FinnyApp.DAL.Database.Entities;
using FinnyApp.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinnyApp.Web.Controllers
{
    public class SubUserController : Controller
    {
        private readonly DataContext _context;
        public SubUserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("/sublogin")]
        public IActionResult SubLogin()
        {
            return View();
        }

        [HttpPost("/sublogin")]
        public async Task<IActionResult> SubLogin(SubUserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.UserTable.AnyAsync(u => u.Email == model.Email);
            var subUser = await _context.SubUserTable.AnyAsync(u => u.PIN == model.PIN);

            if (user && subUser)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, "SubUser")
                };

                var identity = new ClaimsIdentity(claims, "FinnySubUserCookie");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("FinnySubUserCookie", principal);

                return Content("Login successful");
            }
            ModelState.AddModelError(string.Empty, "No User Found!");
            return View();
        }

        [Authorize(AuthenticationSchemes = "FinnySubUserCookie")]
        [HttpGet("/Playground")]
        public IActionResult Playground()
        {
            return View();
        }
    }
}
