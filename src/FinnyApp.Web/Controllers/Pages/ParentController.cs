
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
        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }

        [Route("/Admin")]
        [HttpPost]
        public async Task<IActionResult> Admin(AdminViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _parentService.CreateChild(model.Name, model.PIN);

            return RedirectToAction(nameof(Admin));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(AdminViewModel model)
        {
            if (!ModelState.IsValid || model.ChildId <= 0 || model.RewardAmount < 0)
                return RedirectToAction(nameof(Tasking));

            await _parentService.CreateTask(
                model.Title,
                model.Detail,
                model.ChildId,
                model.RewardAmount
            );

            return RedirectToAction(nameof(Tasking));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(AdminViewModel model)
        {
            if (!ModelState.IsValid ||
                model.TaskId <= 0 ||
                model.ChildId <= 0 ||
                model.RewardAmount < 0)
            {
                return RedirectToAction(nameof(Tasking));
            }

            await _parentService.UpdateTask(
                model.TaskId,
                model.Title,
                model.Detail,
                model.ChildId,
                model.RewardAmount
            );

            return RedirectToAction(nameof(Tasking));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            await _parentService.DeleteTask(taskId);

            return RedirectToAction(nameof(Tasking));
        }

        [Route("/Admin/Tasking")]
        [HttpGet]
        public async Task<IActionResult> Tasking()
        {
            var tasks = await _parentService.GetParentTasks();
            var children = await _parentService.GetParentChildren();

            ViewBag.Children = children;

            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal(AdminViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Admin));

            await _parentService.CreateGoal(model.Title, model.Amount);

            return RedirectToAction(nameof(Admin));
        }

        [Route("/Admin/Goaling")]
        [HttpGet]
        public IActionResult Goaling()
        {
            return View();
        }
    }
}
