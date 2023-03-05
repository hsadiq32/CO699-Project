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

namespace MacroFit.Pages.Users.Progress
{
    public class EditModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public EditModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserProgress UserProgress { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserProgress == null)
            {
                return NotFound();
            }

            var userprogress =  await _context.UserProgress.FirstOrDefaultAsync(m => m.Id == id);
            if (userprogress == null)
            {
                return NotFound();
            }
            UserProgress = userprogress;
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

            _context.Attach(UserProgress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProgressExists(UserProgress.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserProgressExists(int id)
        {
          return _context.UserProgress.Any(e => e.Id == id);
        }
    }
}
