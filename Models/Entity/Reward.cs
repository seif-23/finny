namespace FinnyApp.Models.Entity
{
    public class Reward
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Title { get; set; } = "Add a Reward Title";
        public int FinniesCost { get; set; }

        public required Parent Parent { get; set; }
    }
}
