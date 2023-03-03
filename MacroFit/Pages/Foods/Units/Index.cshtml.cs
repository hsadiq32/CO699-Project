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
    public class IndexModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public IndexModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        public IList<FoodUnit> FoodUnit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FoodUnits != null)
            {
                FoodUnit = await _context.FoodUnits.ToListAsync();
            }
        }
    }
}
