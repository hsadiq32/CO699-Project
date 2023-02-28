namespace MacroFit.Models
{
    public class FoodUnit
    {
        public int Id { get; set; } // PascalCase for property names 
        public string Name { get; set; }
        public string SymbolName { get; set; }
        public double GramsConversion { get; set; } = 0;

    }
}
