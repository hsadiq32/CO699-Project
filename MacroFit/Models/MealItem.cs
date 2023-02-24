namespace MacroFit.Models
{
    public class MealItem // Singular name for entity class 
    {
        public int Id { get; set; } // PascalCase for property names 
        public int MealId { get; set; } // MealId instead of Meal ID as foreign key name 
        public int FoodId { get; set; } // FoodId instead of Food ID as foreign key name 
        public double Quantity { get; set; } // Double type for decimal numbers 

        // Navigation properties

        public Meal Meal { get; set; } // Singular name for navigation property 
        public Food Food { get; set; } // Singular name for navigation property 

    }
}
