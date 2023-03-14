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
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MacroFit.Pages.Log
{
    public class CreateModel : PageModel
    {
        private readonly MacroFitContext _context;

        public CreateModel(MacroFitContext context)
        {
            _context = context;
        }
        public string ParamMealType { get; set; } = null;
        public string? Barcode { get; set; }
        private const string dateTimeLayout = "dd-MM-yyyy HH:mm";
        [BindProperty]
        public int? FoodId { get; set; }
        [BindProperty]
        public FoodLog FoodLog { get; set; }
        [BindProperty]
        public Food Food { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            FoodLog = new FoodLog();
            FoodLog.DateTime = DateTime.Now;
            FoodLog.HungerLevel = HungerLevel.ModeratelyHungry;
            var queryString = HttpContext.Request.Query;
            foreach (var key in queryString.Keys)
            {
                switch (key.ToLower())
                {
                    case "mealtype":
                        ParamMealType = queryString[key];
                        break;
                    case "date":
                        if (DateTime.TryParse(queryString[key], out var dateValue))
                        {
                            FoodLog.DateTime = dateValue;
                        }
                        break;
                    case "foodid":
                        int.TryParse(queryString[key], out var foodIdValue);
                        FoodId = foodIdValue;
                        Food = await _context.Foods
                            .Include(fl => fl.FoodUnit)
                            .FirstOrDefaultAsync(m => m.Id == FoodId);

                        // check if a previous FoodLog with the same FoodId exists
                        var prevFoodLog = await _context.FoodLogs
                            .FirstOrDefaultAsync(fl => fl.Food == Food);

                        if (prevFoodLog != null)
                        {
                            // set the FoodLog.Amount to the value of the previous FoodLog
                            FoodLog.Amount = prevFoodLog.Amount;
                            FoodLog.HungerLevel = prevFoodLog.HungerLevel;
                        }
                        break;
                    case "barcode":
                        Barcode = queryString[key];
                        break;
                    default:
                        // handle any unknown query string parameters
                        break;
                }
            }
            
            FoodLog.DateTime = DateTime.ParseExact(FoodLog.DateTime.ToString(dateTimeLayout), dateTimeLayout, CultureInfo.InvariantCulture);
            FoodLog.Type = typeParser(ParamMealType, FoodLog.DateTime);
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
            if(FoodId != null)
            {
                //Automatically add Food
                Food = await _context.Foods.FirstOrDefaultAsync(m => m.Id == FoodId);
                if (Food == null)
                {
                    // Handle the case where the food does not exist
                    return NotFound();
                }
            }
            else
            {
                //Manually add Food and Food Unit
                var existingFoodUnit = await _context.FoodUnits.FirstOrDefaultAsync(
                    fu => fu.Name == Food.FoodUnit.Name &&
                    fu.SymbolName == Food.FoodUnit.SymbolName &&
                    fu.GramsConversion == Food.FoodUnit.GramsConversion);

                // Use the existing FoodUnit or create a new one
                var foodUnit = existingFoodUnit ?? new FoodUnit
                {
                    Name = Food.FoodUnit.Name,
                    SymbolName = Food.FoodUnit.SymbolName,
                    GramsConversion = Food.FoodUnit.GramsConversion
                };

                // Set the FoodUnit property of the Food object
                Food.FoodUnit = foodUnit;
            }


            FoodLog.Food = Food;
            _context.FoodLogs.Add(FoodLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public MealType typeParser(string type, DateTime dateTime)
        {
            if (type != null)
            {
                return (type.ToLower()) switch
                {
                    "breakfast" => MealType.Breakfast,
                    "lunch" => MealType.Lunch,
                    "dinner" => MealType.Dinner,
                    "snack" => MealType.Snack,
                };
            }

            // Estimate meal type based on time
            int hour = dateTime.Hour;
            if (hour >= 6 && hour < 10)
            {
                return MealType.Breakfast;
            }
            else if (hour >= 10 && hour < 14)
            {
                return MealType.Lunch;
            }
            else if (hour >= 14 && hour < 18)
            {
                return MealType.Snack;
            }
            else if (hour >= 18 && hour < 22)
            {
                return MealType.Dinner;

            }
            return MealType.Lunch;
        }
    }
}
