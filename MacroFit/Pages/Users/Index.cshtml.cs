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

namespace MacroFit.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public IndexModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        public IList<IdentityUser> UserDetails { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Where(us => us.Id == userId)
                .ToListAsync();

            if (user == null)
            {
                return NotFound();
            }

            UserDetails = user;
            return Page();
        }
    }
}
