using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FinnyApp.DAL.Database.Entities
{
    public class suGoalProgress
    {
        public int ID { get; set; }
        public int SubUserID { get; set; }
        public int GoalID { get; set; }
        public int TasksProgress { get; set; }

        public required SubUser SubUser {  get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public required Goal Goal { get; set; }
    }
}
