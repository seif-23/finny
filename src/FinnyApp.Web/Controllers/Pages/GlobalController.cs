
using Microsoft.AspNetCore.Mvc;
using FinnyApp.BLL.Services;
using FinnyApp.Web.Models.ModelViews;

namespace FinnyApp.Web.Controllers.Pages
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
                await _authService.Join(model.Name, model.Email, model.Password);
                return RedirectToAction("Admin", "Parent");
            }

            return View(model);
        }

        [Route("/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var parent = await _authService.GetParentByCredentials(
                model.Email,
                model.Password);

            if (parent == null)
            {
                return Content("Wrong Password");
            }

            HttpContext.Session.SetInt32("ParentID", parent.ID);

            return RedirectToAction("Admin", "Parent");
        }

        [Route("/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
