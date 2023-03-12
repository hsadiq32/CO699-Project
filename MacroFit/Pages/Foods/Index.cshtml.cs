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


            return Page();
        }
    }

}
