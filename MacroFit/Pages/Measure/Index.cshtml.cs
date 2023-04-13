using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MacroFit.Pages.Measure
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public IndexModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        public IList<UserProgress> UserProgress { get; set; } = default!;

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.UserProgress != null)
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return NotFound();
                }
                User = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == userId);
                if (User == null)
                {
                    return NotFound();
                }

                UserProgress = await _context.UserProgress
                    .Where(us => us.Account == User)
                    .ToListAsync();

                User.DateOfBirth = User.DateOfBirth.Date;
            }
            return Page();
        }
    }
}
