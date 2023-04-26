using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MacroFit.Services;

namespace MacroFit.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;
        private readonly EatingTimeRecommendationService _eatingTimeRecommendationService;
        public bool LoginCheck = false;
        public IndexModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
            _eatingTimeRecommendationService = new EatingTimeRecommendationService();

        }

        public IList<FoodLog> FoodLog { get; set; } = default!;
        public DateTime SelectedDate { get; set; } = DateTime.Now.Date;
        public IList<MacronutrientData> MacronutrientData { get; set; } = new List<MacronutrientData>();
        public User User { get; set; }
        public List<DateTime> RecommendedEatingTimes { get; set; }

        public async Task<IActionResult> OnGetAsync(DateTime? date)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                User = await _context.Accounts
                    .Include(fl => fl.UserSettings)
                    .FirstOrDefaultAsync(u => u.Id == userId);
                if (User != null)
                {
                    LoginCheck = true;
                    if (date.HasValue)
                    {
                        SelectedDate = date.Value.Date;
                    }

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

                }
            }

            return Page();
        }

        public double GramsConverter(double value, double percentage, double conversion)
        {
            return (value / 100 * percentage)/ conversion;
        }

    }
}