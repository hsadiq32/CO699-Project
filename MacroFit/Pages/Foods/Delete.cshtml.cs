using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;

namespace MacroFit.Pages.Foods
{
    public class DeleteModel : PageModel
    {
        private readonly MacroFitContext _context;

        public DeleteModel(MacroFitContext context)
        {
            _context = context;
        }

        [BindProperty]
      public FoodLog FoodLog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FoodLogs == null)
            {
                return NotFound();
            }

            var foodlog = await _context.FoodLogs.FirstOrDefaultAsync(m => m.Id == id);

            if (foodlog == null)
            {
                return NotFound();
            }
            else 
            {
                FoodLog = foodlog;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.FoodLogs == null)
            {
                return NotFound();
            }
            var foodlog = await _context.FoodLogs.FindAsync(id);

            if (foodlog != null)
            {
                FoodLog = foodlog;
                _context.FoodLogs.Remove(FoodLog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
