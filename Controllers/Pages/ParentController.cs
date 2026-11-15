using FinnyApp.Database;
using FinnyApp.Models.Entity;
using FinnyApp.Models.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.Controllers.Pages
{
    public class ParentController : Controller
    {
        private readonly DContext _context;
        public ParentController(DContext context)
        {
            _context = context;
        }


        [Route("/Admin")]
        [HttpPost]
        public async Task<IActionResult> Admin(AdminViewModel model)
        {
            var parent = await _context.ParentTable.FirstOrDefaultAsync(n => n.Email == "dal@mail.com");
           
            if (ModelState.IsValid)
            {
                var kid = new Child { Name = model.Name, PIN = model.PIN, Parent = parent};
                _context.ChildTable.Add(kid);
                await _context.SaveChangesAsync();
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
            var parent = await _context.ParentTable.FirstOrDefaultAsync(n => n.Email == "dal@mail.com");

            if (ModelState.IsValid)
            {
                var task = new Tasks { Parent = parent, Title = model.Title, Description = model.Detail};
                _context.TaskTable.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Admin");
            }
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal(AdminViewModel model)
        {
            var parent = await _context.ParentTable.FirstOrDefaultAsync(n => n.Email == "dal@mail.com");

            if (ModelState.IsValid)
            {
                var goal = new Goal { Parent = parent, Title = model.Title, TaskAmountToFinish = model.Amount};
                _context.GoalTable.Add(goal);
                await _context.SaveChangesAsync();
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
