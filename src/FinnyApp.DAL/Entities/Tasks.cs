namespace FinnyApp.DAL.Entities
{
    public class Tasks
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Title { get; set; } = "Add Title";
        public string Description { get; set; } = "Add Description";
        public int RewardAmount { get; set; }

        public required Parent Parent { get; set; }
    }
}
