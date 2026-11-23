using FinnyApp.DAL.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.DAL.Database
{
    public class DataContext: DbContext
    {
        public DbSet<User> UserTable { get; set; }
        public DbSet<SubUser> SubUserTable { get; set; }
        public DbSet<Goal> GoalTable { get; set; }
        public DbSet<Reward> RewardTable { get; set; }
        public DbSet<Entities.Task> TaskTable { get; set; }
        public DbSet<suGoalProgress> suGoalProgressTable { get; set; }
        public DbSet<suRewardSelection> suRewardSelectionTable { get; set; }
        public DbSet<suTaskProgress> suTaskProgressTable { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
