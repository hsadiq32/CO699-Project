namespace MacroFit.Models
{
    public class UserProgress  // Singular name for entity class 
    {
        public int Id { get; set; }  // PascalCase for property names
        public DateTime Date { get; set; }  // DateTime type for date
        public double Weight { get; set; }  // Double type for decimal numbers
        public double BodyFatPercentage { get; set; }
        public double MuscleMass { get; set; }
        public double WaterPercentage { get; set; }
        public double BoneMass { get; set; }
        public int BodyAge { get; set; }

        // Navigation properties

        public User User { get; set; }  // Singular name for navigation property 

    }
}
