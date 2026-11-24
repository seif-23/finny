
namespace FinnyApp.Web.Models.ModelViews
{
    public class AdminViewModel
    {
        public string? Name { get; set; }
        public string? PIN { get; set; }

        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public int ChildId { get; set; }
        public int RewardAmount { get; set; }

        public int Amount { get; set; }
    }
}
