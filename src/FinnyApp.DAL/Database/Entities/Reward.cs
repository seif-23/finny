namespace FinnyApp.DAL.Database.Entities
{
    public class Reward
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; } = "Add a Reward Title";
        public int FinniesCost { get; set; }

        public required User User { get; set; }
    }
}
