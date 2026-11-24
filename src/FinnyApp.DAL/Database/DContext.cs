
using FinnyApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.DAL.Database
{
    public class DContext : DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Parent>().HasData(
                new Parent
                {
                    ID = 1,
                    Name = "Demo Parent",
                    Email = "demo@finny.com",
                    Password = "123456"
                });

            modelBuilder.Entity<Child>().HasData(
                new
                {
                    ID = 1,
                    ParentID = 1,
                    Name = "Demo Child",
                    PIN = "1234",
                    Finnies = 100
                });

            modelBuilder.Entity<Goal>().HasData(
                new
                {
                    ID = 1,
                    ParentID = 1,
                    Title = "Complete 1 Task",
                    TaskAmountToFinish = 1
                });

            modelBuilder.Entity<Reward>().HasData(
                new
                {
                    ID = 1,
                    ParentID = 1,
                    Title = "Extra Play Time",
                    FinniesCost = 50
                });

            modelBuilder.Entity<Tasks>().HasData(
                new
                {
                    ID = 1,
                    ParentID = 1,
                    Title = "Clean Your Room",
                    Description = "Keep your room clean and organized",
                    RewardAmount = 20
                });

            modelBuilder.Entity<ChildGoalProgress>().HasData(
                new
                {
                    ID = 1,
                    ChildID = 1,
                    GoalID = 1,
                    TasksProgress = 0
                });

            modelBuilder.Entity<ChildRewardSelection>().HasData(
                new
                {
                    ID = 1,
                    ChildID = 1,
                    RewardID = 1,
                    Status = false
                });

            modelBuilder.Entity<ChildTaskProgress>().HasData(
                new
                {
                    ID = 1,
                    TaskID = 1,
                    ChildID = 1,
                    Status = false
                });
        }
    }
}
