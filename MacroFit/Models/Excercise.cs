namespace MacroFit.Models
{
    public class Exercise // Singular name for entity class 
    {
        public int Id { get; set; } // PascalCase for property names 
        public int UserId { get; set; } // UserId instead of User ID as foreign key name 
        public DateTime Date { get; set; } // DateTime type for date
        public ExerciseType Type { get; set; } // Enum type for exercise type
        public double Duration { get; set; } // Double type for decimal numbers
        public ExerciseIntensity Intensity { get; set; } // Enum type for exercise intensity

        // Navigation properties

        public User User { get; set; } // Singular name for navigation property 

    }

    public enum ExerciseType
    {
        Cardiovascular,
        StrengthTraining,
        FlexibilityTraining,
        BalanceTraining
    }

    public enum ExerciseIntensity
    {
        LowIntensity,
        ModerateIntensity,
        HighIntensity
    }
}
