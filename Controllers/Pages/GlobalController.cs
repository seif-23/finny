using Microsoft.AspNetCore.Mvc;
using FinnyApp.Database;
using FinnyApp.Models.ModelViews;
using FinnyApp.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.Controllers.Pages
{
    public class GlobalController : Controller
    {
        private readonly DContext _context;
        public GlobalController(DContext context)
        {
            _context = context;
        }
        public IActionResult Home()
        {
            return View();
        }

        [Route("Join")]
        [HttpGet]
        public IActionResult Join()
        {
            return View();
        }


        [Route("/Join")]
        [HttpPost]
        public async Task<IActionResult> Join(JoinViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Parent { Email = model.Email, Name = model.Name, Password = model.Password};
                _context.ParentTable.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Admin", "Parent");
            }
            return View(model);
        }

        [Route("/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var users = await _context.ParentTable.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (users == null || users.Password != model.Password)
            {
                return Content("Wrong Password");
            }
            else
            {
                return RedirectToAction("Admin", "Parent");
            }
        }

        [Route("/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
