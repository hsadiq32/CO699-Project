using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;

namespace MacroFit.Pages.Exercises
{
    public class DetailsModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public DetailsModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

      public UserExercise UserExercise { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserExercises == null)
            {
                return NotFound();
            }

            var userexercise = await _context.UserExercises.FirstOrDefaultAsync(m => m.Id == id);
            if (userexercise == null)
            {
                return NotFound();
            }
            else 
            {
                UserExercise = userexercise;
            }
            return Page();
        }
    }
}
