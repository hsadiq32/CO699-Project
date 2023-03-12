using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MacroFit.Data;
using MacroFit.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MacroFit.Pages.Measure
{
    public class CreateModel : PageModel
    {
        private readonly MacroFitContext _context;
        private readonly IHostEnvironment _environment;

        public CreateModel(MacroFitContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserProgress UserProgress { get; set; }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            UserProgress.Account = user;

            var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", "images", userId);

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            if (UploadedFile != null && UploadedFile.Length != 0)
            {
                var timestamp = DateTime.UtcNow.Ticks;
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(UploadedFile.FileName);
                var fileExtension = Path.GetExtension(UploadedFile.FileName);
                var uniqueFileName = $"{fileNameWithoutExtension}_{timestamp}{fileExtension}";
                UserProgress.ProgressImage = Path.Combine(userId, uniqueFileName);

                string targetFileName = Path.Combine(imagePath, uniqueFileName);
                using (var stream = new FileStream(targetFileName, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(stream);
                }
            }

            _context.UserProgress.Add(UserProgress);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
