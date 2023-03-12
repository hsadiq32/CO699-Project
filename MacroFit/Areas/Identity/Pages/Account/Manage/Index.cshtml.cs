// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MacroFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MacroFit.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public IndexModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty, Required]
        public UserSettings UserSettings { get; set; }

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
                userSettings = new UserSettings();

                // Save the changes to the database
                await _context.SaveChangesAsync();

            }

            UserSettings = userSettings;
            return Page();
        }


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

            StatusMessage = "Changes saved successfully.";
            return Page();
        }

        private bool UserSettingsExists(int id)
        {
            return _context.UserSettings.Any(e => e.Id == id);
        }
    }
}
