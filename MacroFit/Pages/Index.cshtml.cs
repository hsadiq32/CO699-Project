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

namespace MacroFit.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;
        public bool LoginCheck = false;
        public IndexModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        public IList<User> UserDetails { get;set; } = default!;

        public UserSettings UserSettings { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _context.Accounts
                    .Where(us => us.Id == userId)
                    .ToListAsync();

                if (user != null)
                {
                    UserDetails = user;
                    LoginCheck = true;
                }
            }
            return Page();
        }
    }
}
