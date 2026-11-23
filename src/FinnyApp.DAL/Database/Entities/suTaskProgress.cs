using Microsoft.EntityFrameworkCore;

namespace FinnyApp.DAL.Database.Entities
{
    public class suTaskProgress
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public int SubUserID { get; set; }
        public bool Status { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public required Task Task { get; set; }
        public required SubUser SubUser { get; set; }
    }
}
