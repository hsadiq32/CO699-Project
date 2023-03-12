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

namespace MacroFit.Pages.Foods
{
    public class CreateModel : PageModel
    {
        private readonly MacroFitContext _context;

        public CreateModel(MacroFitContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FoodLog FoodLog { get; set; }
        [BindProperty]
        public Food Food { get; set; }
        [BindProperty]
        public FoodUnit FoodUnit { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

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

            FoodLog.Account = user;
            var existingFoodUnit = await _context.FoodUnits.FirstOrDefaultAsync(
                fu => fu.Name == FoodUnit.Name &&
                fu.SymbolName == FoodUnit.SymbolName &&
                fu.GramsConversion == FoodUnit.GramsConversion);

            // Use the existing FoodUnit or create a new one
            var foodUnit = existingFoodUnit ?? new FoodUnit
            {
                Name = FoodUnit.Name,
                SymbolName = FoodUnit.SymbolName,
                GramsConversion = FoodUnit.GramsConversion
            };

            // Set the FoodUnit property of the Food object
            Food.FoodUnit = foodUnit;

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}


            FoodLog.Food = Food;

            _context.FoodLogs.Add(FoodLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
