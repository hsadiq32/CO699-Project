namespace MacroFit.Models
{
    public class MacronutrientData
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public DateTime ConsumptionTime { get; set; }
        public double Amount { get; set; }
        public string AmountSymbol { get; set; }
        public double Calories { get; set; }
        public double Carbohydrates { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }
        public double Sugar { get; set; }
        public double Sodium { get; set; }
        public double Fibre { get; set; }
        public MealType MealType { get; set; }


        public MacronutrientData(FoodLog foodLog)
        {
            if (foodLog == null)
            {
                throw new ArgumentNullException(nameof(foodLog));
            }

            Id = foodLog.Id;
            FoodName = foodLog.Food.Name;
            ConsumptionTime = foodLog.DateTime;
            Amount = foodLog.Amount * foodLog.Food.FoodUnit.GramsConversion;
            AmountSymbol = foodLog.Food.FoodUnit.SymbolName;
            Calories = (foodLog.Food.Calories / 100) * foodLog.Amount;
            Carbohydrates = (foodLog.Food.Carbohydrates / 100) * foodLog.Amount;
            Fat = (foodLog.Food.Fat / 100) * foodLog.Amount;
            Protein = (foodLog.Food.Protein / 100) * foodLog.Amount;
            Sugar = (foodLog.Food.Sugar / 100) * foodLog.Amount;
            Sodium = (foodLog.Food.Sodium / 100) * foodLog.Amount;
            Fibre = (foodLog.Food.Fibre / 100) * foodLog.Amount;
            MealType = foodLog.Type;
        }
    }
}
