using MacroFit.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MacroFit.Services;

namespace MacroFit.Pages.Foods
{
    public class IndexModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;
        private readonly EatingTimeRecommendationService _eatingTimeRecommendationService;


        public IndexModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
            _eatingTimeRecommendationService = new EatingTimeRecommendationService();

        }

        public IList<FoodLog> FoodLog { get; set; } = default!;
        public DateTime SelectedDate { get; set; } = DateTime.Now.Date;
        public DateTime LogDateTime { get; set; }
        public IList<MacronutrientData> MacronutrientData { get; set; } = new List<MacronutrientData>();
        public  User User { get; set; }
        public List<DateTime> RecommendedEatingTimes { get; set; }

        public async Task<IActionResult> OnGetAsync(DateTime? date)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound();
            }

            User = await _context.Accounts
                .Include(fl => fl.UserSettings)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (User == null)
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
                .Where(fl => fl.Account == User && fl.DateTime.Date == SelectedDate)
                .ToListAsync();


            MacronutrientData = await _context.FoodLogs
                .Include(fl => fl.Food)
                    .ThenInclude(f => f.FoodUnit)
                .Where(fl => fl.Account == User && fl.DateTime.Date == SelectedDate)
                .Select(fl => new MacronutrientData(fl))
                .ToListAsync();

            // Retrieve user's food logs. Replace with actual data retrieval.
            var userFoodLogs = new List<FoodLog>();

            // Get recommended eating times
            RecommendedEatingTimes = _eatingTimeRecommendationService.GetOptimalEatingTimes(userFoodLogs);

            return Page();
        }

        public double GramsConverter(double value, double conversion)
        {
            return ((User.UserSettings.CalorieGoal / 100) * value) / conversion;
        }

    }
}
