using Microsoft.AspNetCore.Mvc;
using FinnyApp.BLL.Services;
using FinnyApp.Web.ViewModels;

namespace FinnyApp.Web.Controllers
{
    public class GlobalController : Controller
    {
        private readonly AuthService _authService;
        public GlobalController(AuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet("Join")]
        public IActionResult Join()
        {
            return View();
        }

        [HttpPost("Join")]
        public async Task<IActionResult> Join(JoinViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _authService.Join(model.Name, model.Email, model.Password);
                return RedirectToAction("Admin", "Parent");
            }
            return View(model);
        }

        [Route("/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var success = await _authService.Login(model.Email, model.Password);

            if (!success)
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
