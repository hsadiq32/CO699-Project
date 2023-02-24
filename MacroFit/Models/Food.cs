namespace MacroFit.Models
{
    public class Food // Singular name for entity class 
    {
        public int Id { get; set; } // PascalCase for property names 
        public string Name { get; set; }
        public long BarcodeNumber { get; set; } // Long type for large numbers 
        public double ServingSize { get; set; } // Double type for decimal numbers 
        public int Calories { get; set; }
        public double Protein { get; set; }
        public double Carbohydrates { get; set; }
        public double Fat { get; set; }
        public double FatSat { get; set; }
        public double Fibre { get; set; }
        public double Sugar { get; set; }

        // Navigation properties

        public ICollection<MealItem> MealItems { get; set; } // ICollection<T> type for navigation properties 

    }
}
