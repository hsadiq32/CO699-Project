using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MacroFit.Data;
using MacroFit.Models;

namespace MacroFit.Pages.Users.Settings
{
    public class CreateModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public CreateModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserSettings UserSettings { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UserSettings.Add(UserSettings);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
