using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MacroFit.Pages.Users.Settings
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public EditModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserSettings UserSettings { get; set; } = default!;

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null || _context.UserSettings == null)
        //    {
        //        return NotFound();
        //    }

        //    var usersettings =  await _context.UserSettings.FirstOrDefaultAsync(m => m.Id == id);
        //    if (usersettings == null)
        //    {
        //        return NotFound();
        //    }
        //    UserSettings = usersettings;
        //    return Page();
        //}

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound();
            }

            var userSettings = await _context.Accounts
                .Where(us => us.Id == userId)
                .Select(us => us.UserSettings)
                .FirstOrDefaultAsync();
            if (userSettings == null)
            {
                return NotFound();
            }

            UserSettings = userSettings;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSettingsExists(UserSettings.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["Message"] = "Changes saved successfully.";
            return Page();
        }

        private bool UserSettingsExists(int id)
        {
          return _context.UserSettings.Any(e => e.Id == id);
        }
    }
}
