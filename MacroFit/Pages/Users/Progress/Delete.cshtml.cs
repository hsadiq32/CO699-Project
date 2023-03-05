using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;
using Microsoft.Extensions.Hosting;

namespace MacroFit.Pages.Users.Progress
{
    public class DeleteModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;
        private readonly IHostEnvironment _environment;

        public DeleteModel(MacroFit.Data.MacroFitContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UserProgress == null)
            {
                return NotFound();
            }

            var userprogress = await _context.UserProgress.FindAsync(id);

            if (userprogress != null)
            {
                UserProgress = userprogress;

                // Delete the image file from physical storage
                if (!string.IsNullOrEmpty(UserProgress.ProgressImage))
                {
                    string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot", "images", UserProgress.ProgressImage);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.UserProgress.Remove(UserProgress);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

    }
}
