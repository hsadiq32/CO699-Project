namespace MacroFit.Models
{
    public class Meal // Singular name for entity class 
    {
        public int Id { get; set; } // PascalCase for property names 
        public int UserId { get; set; } // UserId instead of User ID as foreign key name 
        public DateTime DateTime { get; set; } // DateTime type for date and time 
        public MealType Type { get; set; } // Enum type for meal type 
        public HungerLevel HungerLevel { get; set; } // Enum type for hunger level 

        // Navigation properties

        public User User { get; set; } // Singular name for navigation property 
        public ICollection<MealItem> MealItems { get; set; } // ICollection<T> type for navigation properties 

    }

    public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack
    }

    public enum HungerLevel
    {
        NotHungry,
        SlightlyHungry,
        ModeratelyHungry,
        VeryHungry,
        ExtremelyHungry
    }
}
