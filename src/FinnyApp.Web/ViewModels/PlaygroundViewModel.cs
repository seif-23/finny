using FinnyApp.DAL.Database;
using FinnyApp.DAL.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.Web.ViewModels
{
    public class PlaygroundViewModel
    {
        private readonly DataContext _context;
        public PlaygroundViewModel(DataContext context)
        {
            _context = context;
        }

        public async Task<List<DAL.Database.Entities.Task>> SeeTasks()
        {
            var tasks = await _context.TaskTable.ToListAsync();
            return tasks;
        }
    }
}
