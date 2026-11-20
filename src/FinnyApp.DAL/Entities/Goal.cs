namespace FinnyApp.DAL.Entities
{
    public class Goal
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Title { get; set; } = "Add Title";
        public int TaskAmountToFinish { get; set; }

        public required Parent Parent { get; set; }
    }
}
