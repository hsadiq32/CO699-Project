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

namespace MacroFit.Pages.Users.Logs
{
    public class IndexModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public IndexModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        public IList<FoodLog> FoodLog { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
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
            if (_context.FoodLogs != null)
            {
                FoodLog = await _context.FoodLogs
                    .Where(us => us.Account == user)
                    .ToListAsync();
            }
            return Page();
        }
    }
}
