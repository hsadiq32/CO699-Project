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

namespace MacroFit.Pages.Foods.Units
{
    public class EditModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public EditModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FoodUnit FoodUnit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FoodUnits == null)
            {
                return NotFound();
            }

            var foodunit =  await _context.FoodUnits.FirstOrDefaultAsync(m => m.Id == id);
            if (foodunit == null)
            {
                return NotFound();
            }
            FoodUnit = foodunit;
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

            _context.Attach(FoodUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodUnitExists(FoodUnit.Id))
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

        private bool FoodUnitExists(int id)
        {
          return _context.FoodUnits.Any(e => e.Id == id);
        }
    }
}
