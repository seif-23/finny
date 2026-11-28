namespace FinnyApp.Web.ViewModels
{
    public class DashboardViewModel
    {
        public CreateSubUser CreateSubUser { get; set; } = new();
        public CreateTask CreateTask { get; set; } = new();
        public CreateGoal CreateGoal { get; set; } = new();

    }

    public class CreateSubUser
    {
        public string? Name { get; set; }
        public string? PIN { get; set; }
    }

    public class CreateTask
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class CreateGoal
    {
        public string? Title { get; set; }
    }
}
