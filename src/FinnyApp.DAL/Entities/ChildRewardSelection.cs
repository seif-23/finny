using Microsoft.EntityFrameworkCore;

namespace FinnyApp.DAL.Entities
{
    public class ChildRewardSelection
    {
        public int ID { get; set; }
        public int ChildID { get; set; }
        public int RewardID { get; set; }
        public bool Status { get; set; }

        public required Child Child { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public required Reward Reward { get; set; }
    }
}
