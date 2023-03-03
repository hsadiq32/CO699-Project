using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;

namespace MacroFit.Pages.Foods.Units
{
    public class DetailsModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public DetailsModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

      public FoodUnit FoodUnit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FoodUnits == null)
            {
                return NotFound();
            }

            var foodunit = await _context.FoodUnits.FirstOrDefaultAsync(m => m.Id == id);
            if (foodunit == null)
            {
                return NotFound();
            }
            else 
            {
                FoodUnit = foodunit;
            }
            return Page();
        }
    }
}
