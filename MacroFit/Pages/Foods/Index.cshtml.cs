using MacroFit.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MacroFit.Pages.Foods
{
    public class IndexModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public IndexModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        public IList<FoodLog> FoodLog { get; set; } = default!;
        public DateTime SelectedDate { get; set; } = DateTime.Now.Date;
        public IList<MacronutrientData> BreakfastData { get; set; } = new List<MacronutrientData>();
        public IList<MacronutrientData> LunchData { get; set; } = new List<MacronutrientData>();
        public IList<MacronutrientData> DinnerData { get; set; } = new List<MacronutrientData>();
        public IList<MacronutrientData> SnackData { get; set; } = new List<MacronutrientData>();
        public IList<MacronutrientData> OverallData { get; set; } = new List<MacronutrientData>();

        public UserSettings UserSettings { get; set; }

        public async Task<IActionResult> OnGetAsync(DateTime? date)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            if (date.HasValue)
            {
                SelectedDate = date.Value.Date;
            }

            FoodLog = await _context.FoodLogs
                .Include(fl => fl.Food)
                    .ThenInclude(f => f.FoodUnit)
                .Where(fl => fl.Account == user && fl.DateTime.Date == SelectedDate)
                .ToListAsync();

            foreach (var log in FoodLog)
            {
                var data = new MacronutrientData(log);
                switch (log.Type)
                {
                    case MealType.Breakfast:
                        BreakfastData.Add(data);
                        break;
                    case MealType.Lunch:
                        LunchData.Add(data);
                        break;
                    case MealType.Dinner:
                        DinnerData.Add(data);
                        break;
                    case MealType.Snack:
                        SnackData.Add(data);
                        break;
                }
                OverallData.Add(data);
            }
            


            return Page();
        }
    }


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


        public MacronutrientData(FoodLog foodLog)
        {
            Id = foodLog.Id;
            FoodName = foodLog.Food.Name;
            ConsumptionTime = foodLog.DateTime;
            Amount = foodLog.Amount * (foodLog.Food.FoodUnit.GramsConversion * (foodLog.Food.ServingSize / 100));
            AmountSymbol = foodLog.Food.FoodUnit.SymbolName;
            Calories = (foodLog.Food.Calories/100) * foodLog.Amount;
            Carbohydrates = (foodLog.Food.Carbohydrates / 100) * foodLog.Amount;
            Fat = (foodLog.Food.Fat / 100) * foodLog.Amount;
            Protein = (foodLog.Food.Protein / 100) * foodLog.Amount;
            Sugar = (foodLog.Food.Sugar / 100) * foodLog.Amount;
            Sodium = (foodLog.Food.Sodium / 100) * foodLog.Amount;
            Fibre = (foodLog.Food.Fibre / 100) * foodLog.Amount;

        }
    }

}
