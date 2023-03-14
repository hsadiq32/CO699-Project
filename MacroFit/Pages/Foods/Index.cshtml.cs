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
        public DateTime LogDateTime { get; set; }
        public IList<MacronutrientData> MacronutrientData { get; set; } = new List<MacronutrientData>();

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

            DateTime currentTime = DateTime.Now;
            LogDateTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day,
                                                 currentTime.Hour, currentTime.Minute, currentTime.Second);

            FoodLog = await _context.FoodLogs
                .Include(fl => fl.Food)
                    .ThenInclude(f => f.FoodUnit)
                .Where(fl => fl.Account == user && fl.DateTime.Date == SelectedDate)
                .ToListAsync();

            MacronutrientData = await _context.FoodLogs
                .Include(fl => fl.Food)
                    .ThenInclude(f => f.FoodUnit)
                .Where(fl => fl.Account == user && fl.DateTime.Date == SelectedDate)
                .Select(fl => new MacronutrientData(fl))
                .ToListAsync();



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
            Amount = foodLog.Amount * (foodLog.Food.FoodUnit.GramsConversion * (foodLog.Food.ServingSize / 100));
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
