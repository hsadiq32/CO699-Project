namespace MacroFit.Models
{
    public class UserSettings  // Singular name for entity class 
    {
        public int Id { get; set; }  // PascalCase for property names
        public bool DarkModeToggle { get; set; }  // Bool type for boolean values
        public DashboardView DashboardView { get; set; }  // Enum type for dashboard view
        public bool AutoGoal { get; set; }
        public bool MetricToggle { get; set; }
        public ActivityLevel ActivityLevel { get; set; } // Enum type for activity level
        public GoalType GoalType { get; set; } // Enum type for goal type
        public int CalorieGoal { get; set; }
        public double CarbohydratesPercentage { get; set; }
        public double ProteinPercentage { get; set; }
        public double FatPercentage { get; set; }
    }

    public enum DashboardView
    {
        DailyView,
        WeeklyView,
        MonthlyView
    }

    public enum ActivityLevel
    {
        Sedentary,
        Light,
        Moderate,
        Active,
        VeryActive
    }

    public enum GoalType
    {
        LoseWeight,
        MaintainWeight,
        GainWeight
    }
}
