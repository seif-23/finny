using Microsoft.AspNetCore.Mvc;
using FinnyApp.Web.ViewModels;
using FinnyApp.DAL.Database.Entities;
using FinnyApp.DAL.Database;

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
        public IActionResult SubLogin(SubUserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.SubUserTable.Any(u => u.User.Email == model.Email && u.PIN == model.PIN))
                {
                    return Content("SubUser login successful");
                }
            }
            return View();
        }
    }
}
