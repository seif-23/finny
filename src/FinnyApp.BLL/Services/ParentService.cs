using FinnyApp.DAL.Database;
using FinnyApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.BLL.Services
{
    public class ParentService
    {
        private readonly DContext _context;
        public ParentService(DContext context)
        {
            _context = context;
        }

        public async Task CreateChild(string? name, string? pin)
        {
            var parent = await _context.ParentTable.FirstOrDefaultAsync(n => n.Email == "dal@mail.com");

            var kid = new Child { Name = name, PIN = pin, Parent = parent };
            _context.ChildTable.Add(kid);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTask(string? title, string? detail)
        {
            var parent = await _context.ParentTable.FirstOrDefaultAsync(n => n.Email == "dal@mail.com");

            var task = new Tasks { Parent = parent, Title = title, Description = detail };
            _context.TaskTable.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task CreateGoal(string? title, int amount)
        {
            var parent = await _context.ParentTable.FirstOrDefaultAsync(n => n.Email == "dal@mail.com");

            var goal = new Goal { Parent = parent, Title = title, TaskAmountToFinish = amount };
            _context.GoalTable.Add(goal);
            await _context.SaveChangesAsync();
        }
    }
}
