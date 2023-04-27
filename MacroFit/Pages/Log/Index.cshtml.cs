using MacroFit.Models;
using MacroFit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MacroFit.Pages.Log
{
    public class ScannerModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public ScannerModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;

        }

        public User User { get; set; }

        public IList<FoodLog> RecentFoods { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound();
            }

            User = await _context.Accounts
                .Include(fl => fl.UserSettings)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (User == null)
            {
                return NotFound();
            }
            // Since this is a read-only query, AsNoTracking method is used to improve performance by telling EF that it doesn't need to track changes to the returned entities.
            RecentFoods = await _context.FoodLogs
                .Include(f => f.Food)
                .Where(f => f.Account == User)
                .OrderBy(f => f.DateTime)
                .GroupBy(f => f.Food.Id)
                .Select(g => g.First())
                .Take(15)
                .ToListAsync();
            return Page();
        }
    }
}
