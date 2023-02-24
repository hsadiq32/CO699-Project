using System;

namespace MacroFit.Models
{
    public class User // Singular name for entity class [^1^][2]
    {
        public int Id { get; set; } // PascalCase for property names [^2^][1]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; } // DateTime type for date [^1^][2]
        public string Gender { get; set; }
        public double Height { get; set; } // Double type for decimal numbers [^1^][2]
        public double Weight { get; set; }
        public ActivityLevel ActivityLevel { get; set; } // Enum type for activity level [^1^][2]
        public GoalType GoalType { get; set; } // Enum type for goal type [^1^][2]
        public int CalorieGoal { get; set; }
        public double CarbohydratesPercentage { get; set; }
        public double ProteinPercentage { get; set; }
        public double FatPercentage { get; set; }

        // Navigation properties

        public ICollection<Meal> Meals { get; set; } // ICollection<T> type for navigation properties [^1^][2]
        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<Progress> Progresses { get; set; }
        public AppSetting AppSetting { get; set; } // Singular name for navigation property [^1^][2]
        public ICollection<UserFeedback> UserFeedbacks { get; set; }

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
