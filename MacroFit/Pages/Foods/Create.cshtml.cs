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

namespace MacroFit.Pages.Foods
{
    public class CreateModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public CreateModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Food Food { get; set; }

        [BindProperty]
        public FoodUnit FoodUnit { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    // Iterate through the errors and add them to a list
            //    var errorList = new List<string>();
            //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //    {
            //        errorList.Add(error.ErrorMessage);
            //    }

            //    // Pass the error list to the view
            //    ViewData["Errors"] = errorList;

            //    return Page();
            //}


            // Check if there is an existing FoodUnit with the same properties
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

            _context.Foods.Add(Food);
            _context.SaveChanges();

            return RedirectToPage("./Index");

        }

    }
}
