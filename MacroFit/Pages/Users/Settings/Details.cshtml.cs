using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;

namespace MacroFit.Pages.Users.Settings
{
    public class DetailsModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public DetailsModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

      public UserSettings UserSettings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserSettings == null)
            {
                return NotFound();
            }

            var usersettings = await _context.UserSettings.FirstOrDefaultAsync(m => m.Id == id);
            if (usersettings == null)
            {
                return NotFound();
            }
            else 
            {
                UserSettings = usersettings;
            }
            return Page();
        }
    }
}
