using FinnyApp.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.Database
{
    public class DContext: DbContext
    {
        public DbSet<Parent> ParentTable { get; set; }
        public DbSet<Child> ChildTable { get; set; }
        public DbSet<Goal> GoalTable { get; set; }
        public DbSet<Reward> RewardTable { get; set; }
        public DbSet<Tasks> TaskTable { get; set; }
        public DbSet<ChildGoalProgress> ChildGoalProgressTable { get; set; }
        public DbSet<ChildRewardSelection> ChildRewardSelectionTable { get; set; }
        public DbSet<ChildTaskProgress> ChildTaskProgressTable { get; set; }

        public DContext(DbContextOptions<DContext> options) : base(options)
        {

        }
    }
}
