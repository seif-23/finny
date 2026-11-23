using FinnyApp.BLL.Services;
using FinnyApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinnyApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ParentService _parentService;
        public UserController(ParentService parentService)
        {
            _parentService = parentService;
        }


        //customized
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
                return RedirectToAction("Dashboard", "User");
            }
            return View(model);
        }


        [HttpGet("/Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost("/Dashboard")]
        public async Task<IActionResult> Dashboard(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _parentService.CreateChild(model.Name, model.PIN);
                return View(model);
            }
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> CreateTask(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _parentService.CreateTask(model.Title, model.Detail);
                return RedirectToAction("Admin");
            }
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _parentService.CreateGoal(model.Title, model.Amount);
                return RedirectToAction("Admin");
            }
            return RedirectToAction("Admin");
        }

        [Route("/Admin/Tasking")]
        public IActionResult Tasking()
        {
            return View();
        }

        [Route("/Admin/Goaling")]
        public IActionResult Goaling()
        {
            return View();
        }
    }
}
