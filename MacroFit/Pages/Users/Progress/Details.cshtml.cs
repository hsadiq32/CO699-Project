using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;

namespace MacroFit.Pages.Users.Progress
{
    public class DetailsModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public DetailsModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

      public UserProgress UserProgress { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserProgress == null)
            {
                return NotFound();
            }

            var userprogress = await _context.UserProgress.FirstOrDefaultAsync(m => m.Id == id);
            if (userprogress == null)
            {
                return NotFound();
            }
            else 
            {
                UserProgress = userprogress;
            }
            return Page();
        }
    }
}
