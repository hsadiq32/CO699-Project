using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class FoodLog // Singular name for entity class
    {
        public int Id { get; set; } // PascalCase for property names

        [Required(ErrorMessage = "Date Time is required.")]
        [Display(Name = "Date Time")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; } // DateTime type for date and time 

        [Required(ErrorMessage = "Meal type is required")]
        [Display(Name = "Meal Type")]
        public MealType Type { get; set; } // Enum type for meal type 

        [Required(ErrorMessage = "Hunger level is required")]
        [Display(Name = "Hunger Level")]
        public HungerLevel HungerLevel { get; set; } // Enum type for hunger level 

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public double Amount { get; set; } // Double type for decimal numbers 

        // Navigation properties
        [Required(ErrorMessage = "Food is required")]
        [Display(Name = "Food")]
        public Food Food { get; set; } // Link to Food

        [Required(ErrorMessage = "Account is required")]
        [Display(Name = "Account")]
        public Account Account { get; set; } // Singular name for navigation property 
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
