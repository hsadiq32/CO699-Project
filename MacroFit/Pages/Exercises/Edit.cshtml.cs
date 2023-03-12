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

namespace MacroFit.Pages.Exercises
{
    public class EditModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public EditModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserExercise UserExercise { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserExercises == null)
            {
                return NotFound();
            }

            var userexercise =  await _context.UserExercises.FirstOrDefaultAsync(m => m.Id == id);
            if (userexercise == null)
            {
                return NotFound();
            }
            UserExercise = userexercise;
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

            _context.Attach(UserExercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExerciseExists(UserExercise.Id))
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

        private bool UserExerciseExists(int id)
        {
          return _context.UserExercises.Any(e => e.Id == id);
        }
    }
}
