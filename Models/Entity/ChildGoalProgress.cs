using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FinnyApp.Models.Entity
{
    public class ChildGoalProgress
    {
        public int ID { get; set; }
        public int ChildID { get; set; }
        public int GoalID { get; set; }
        public int TasksProgress { get; set; }

        public required Child Child {  get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public required Goal Goal { get; set; }
    }
}
