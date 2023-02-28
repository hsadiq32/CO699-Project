namespace MacroFit.Models
{
    public class FoodLog // Singular name for entity class 
    {
        public int Id { get; set; } // PascalCase for property names 
        public DateTime DateTime { get; set; } // DateTime type for date and time 
        public MealType Type { get; set; } // Enum type for meal type 
        public HungerLevel HungerLevel { get; set; } // Enum type for hunger level 
        public double Amount { get; set; } // Double type for decimal numbers 

        // Navigation properties

        public Food Food { get; set; } // Link to Food
        public User User { get; set; } // Singular name for navigation property 

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
