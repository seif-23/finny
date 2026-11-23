using FinnyApp.BLL.Services;
using FinnyApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinnyApp.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost("/Dashboard/AddSub")]
        public async Task<IActionResult> AddSubUser(AddSubUserViewModel model)
        {
            return View();
        }

        [HttpPost("/Dashboard/AddTask")]
        public async Task<IActionResult> AddTask(AddTaskViewModel model)
        {
            return View();
        }

        [HttpPost("/Dashboard/AddGoal")]
        public async Task<IActionResult> AddGoal(AddGoalViewModel model)
        {
            return View();
        }
    }
}
