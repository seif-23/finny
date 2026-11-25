namespace FinnyApp.DAL.Database.Entities
{
    public class Task
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; } = "Add Title";
        public string Description { get; set; } = "Add Description";
        public int RewardAmount { get; set; }

        public required User User { get; set; }
    }
}
