using FinnyApp.DAL.Database;
using FinnyApp.DAL.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.BLL.Services
{
    public class ParentService
    {
        private readonly DataContext _context;
        public ParentService(DataContext context)
        {
            _context = context;
        }

        public async Task<SubUser> CreateChild(string name, string pin, User user)
        {
            if (_context.UserTable.Select(n => n.Email).FirstOrDefault() == user.Email)
            {
                var kid = new SubUser { Name = name, PIN = pin, User = user };
                _context.SubUserTable.Add(kid);
                await _context.SaveChangesAsync();
                return kid;
            }

            return null;
        }

        public async Task CreateTask(string? title, string? detail)
        {
            var parent = await _context.ParentTable.FirstOrDefaultAsync(n => n.Email == "dal@mail.com");

            var task = new DAL.Entities.Task { Parent = parent, Title = title, Description = detail };
            _context.TaskTable.Add(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task CreateGoal(string? title, int amount)
        {
            var parent = await _context.ParentTable.FirstOrDefaultAsync(n => n.Email == "dal@mail.com");

            var goal = new Goal { Parent = parent, Title = title, TaskAmountToFinish = amount };
            _context.GoalTable.Add(goal);
            await _context.SaveChangesAsync();
        }
    }
}
