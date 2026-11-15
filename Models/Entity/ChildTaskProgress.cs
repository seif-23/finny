using Microsoft.EntityFrameworkCore;

namespace FinnyApp.Models.Entity
{
    public class ChildTaskProgress
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public int ChildID { get; set; }
        public bool Status { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public required Tasks Task { get; set; }
        public required Child Child { get; set; }
    }
}
