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
    public class IndexModel : PageModel
    {
        private readonly MacroFit.Data.MacroFitContext _context;

        public IndexModel(MacroFit.Data.MacroFitContext context)
        {
            _context = context;
        }

        public IList<Food> Food { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Foods != null)
            {
                Food = await _context.Foods.ToListAsync();
            }
        }
    }
}
