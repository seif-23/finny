using Microsoft.EntityFrameworkCore;

namespace FinnyApp.DAL.Database.Entities
{
    public class suRewardSelection
    {
        public int ID { get; set; }
        public int SubUserID { get; set; }
        public int RewardID { get; set; }
        public bool Status { get; set; }

        public required SubUser SubUser { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public required Reward Reward { get; set; }
    }
}
