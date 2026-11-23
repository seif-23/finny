using Microsoft.AspNetCore.Mvc;
using FinnyApp.Web.ViewModels;

namespace FinnyApp.Web.Controllers
{
    public class PlaygroundController : Controller
    {
        public IActionResult Playground()
        {
            return View();
        }

        [HttpPost("/Playground/Task")]
        public IActionResult Task(TaskViewModel model)
        {
            return View();
        }

        [HttpPost("/Playground/Goal")]
        public IActionResult Goal(GoalViewModel model)
        {
            return View();
        }
    }
}
