namespace MacroFit.Models
{
    public class AppSetting  // Singular name for entity class 
    {
        public int Id { get; set; }  // PascalCase for property names
        public int UserId { get; set; }  // UserId instead of User ID as foreign key name
        public bool DarkModeToggle { get; set; }  // Bool type for boolean values
        public DashboardView DashboardView { get; set; }  // Enum type for dashboard view
        public bool AutoGoal { get; set; }
        public bool MetricToggle { get; set; }

        // Navigation properties
        public User User { get; set; }  // Singular name for navigation property 
    }

    public enum DashboardView
    {
        DailyView,
        WeeklyView,
        MonthlyView
    }
}
