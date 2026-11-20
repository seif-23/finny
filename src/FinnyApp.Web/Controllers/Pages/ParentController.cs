using FinnyApp.BLL.Services;
using FinnyApp.Web.Models.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace FinnyApp.Web.Controllers.Pages
{
    public class ParentController : Controller
    {
        private readonly ParentService _parentService;
        public ParentController(ParentService parentService)
        {
            _parentService = parentService;
        }


        [Route("/Admin")]
        [HttpPost]
        public async Task<IActionResult> Admin(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _parentService.CreateChild(model.Name, model.PIN);
                return View(model);
            }
            return View(model);
        }

        [Route("/Admin")]
        [HttpGet]
        public IActionResult Admin()
        {
            return View();
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
