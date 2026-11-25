namespace FinnyApp.DAL.Database.Entities
{
    public class Goal
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; } = "Add Title";
        public int TaskAmountToFinish { get; set; }

        public required User User { get; set; }
    }
}
