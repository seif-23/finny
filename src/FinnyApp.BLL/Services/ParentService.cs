
using FinnyApp.DAL.Database;
using FinnyApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.BLL.Services
{
    public class ParentService
    {
        private readonly DContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ParentService(DContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<Parent> GetCurrentParent()
        {
            var parentId = _httpContextAccessor.HttpContext?.Session.GetInt32("ParentID");

            if (parentId == null)
                throw new InvalidOperationException("No parent is currently logged in.");

            var parent = await _context.ParentTable
                .FirstOrDefaultAsync(p => p.ID == parentId.Value);

            if (parent == null)
                throw new InvalidOperationException("Parent account was not found.");

            return parent;
        }

        private async Task<Child> GetParentChild(int childId, int parentId)
        {
            var child = await _context.ChildTable
                .FirstOrDefaultAsync(c => c.ID == childId && c.ParentID == parentId);

            if (child == null)
                throw new InvalidOperationException("Child was not found.");

            return child;
        }

        public async Task CreateChild(string? name, string? pin)
        {
            var parent = await GetCurrentParent();

            var kid = new Child
            {
                Name = name,
                PIN = pin!,
                Parent = parent
            };

            _context.ChildTable.Add(kid);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTask(string? title, string? detail)
        {
            var parent = await GetCurrentParent();

            var task = new Tasks
            {
                Parent = parent,
                Title = title ?? "Add Title",
                Description = detail ?? "Add Description"
            };

            _context.TaskTable.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTask(string? title, string? detail, int childId, int rewardAmount)
        {
            var parent = await GetCurrentParent();
            var child = await GetParentChild(childId, parent.ID);

            var task = new Tasks
            {
                Parent = parent,
                Title = title ?? "Add Title",
                Description = detail ?? "Add Description",
                RewardAmount = rewardAmount
            };

            _context.TaskTable.Add(task);
            await _context.SaveChangesAsync();

            var progress = new ChildTaskProgress
            {
                Task = task,
                Child = child,
                Status = false
            };

            _context.ChildTaskProgressTable.Add(progress);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChildTaskProgress>> GetParentTasks()
        {
            var parent = await GetCurrentParent();

            return await _context.ChildTaskProgressTable
                .Include(p => p.Task)
                .Include(p => p.Child)
                .Where(p => p.Task.ParentID == parent.ID)
                .ToListAsync();
        }

        public async Task<List<Child>> GetParentChildren()
        {
            var parent = await GetCurrentParent();

            return await _context.ChildTable
                .Where(c => c.ParentID == parent.ID)
                .ToListAsync();
        }

        public async Task UpdateTask(int taskId, string? title, string? detail, int childId, int rewardAmount)
        {
            var parent = await GetCurrentParent();

            var task = await _context.TaskTable
                .FirstOrDefaultAsync(t => t.ID == taskId && t.ParentID == parent.ID);

            if (task == null)
                throw new InvalidOperationException("Task was not found.");

            var child = await GetParentChild(childId, parent.ID);

            task.Title = title ?? "Add Title";
            task.Description = detail ?? "Add Description";
            task.RewardAmount = rewardAmount;

            var progressRecords = await _context.ChildTaskProgressTable
                .Where(p => p.TaskID == taskId)
                .ToListAsync();

            var selectedProgress = progressRecords
                .FirstOrDefault(p => p.ChildID == childId);

            foreach (var progress in progressRecords)
            {
                if (progress.ChildID != childId)
                    _context.ChildTaskProgressTable.Remove(progress);
            }

            if (selectedProgress == null)
            {
                _context.ChildTaskProgressTable.Add(new ChildTaskProgress
                {
                    Task = task,
                    Child = child,
                    Status = false
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(int taskId)
        {
            var parent = await GetCurrentParent();

            var task = await _context.TaskTable
                .FirstOrDefaultAsync(t => t.ID == taskId && t.ParentID == parent.ID);

            if (task == null)
                throw new InvalidOperationException("Task was not found.");

            var progressRecords = await _context.ChildTaskProgressTable
                .Where(p => p.TaskID == taskId)
                .ToListAsync();

            _context.ChildTaskProgressTable.RemoveRange(progressRecords);
            _context.TaskTable.Remove(task);

            await _context.SaveChangesAsync();
        }

        public async Task CreateGoal(string? title, int amount)
        {
            var parent = await GetCurrentParent();

            var goal = new Goal
            {
                Parent = parent,
                Title = title ?? "Add Title",
                TaskAmountToFinish = amount
            };

            _context.GoalTable.Add(goal);
            await _context.SaveChangesAsync();
        }
    }
}
