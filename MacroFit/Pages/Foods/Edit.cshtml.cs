﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;

namespace MacroFit.Pages.Foods
{
    public class EditModel : PageModel
    {
        private readonly MacroFitContext _context;

        public EditModel(MacroFitContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FoodLog FoodLog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FoodLogs == null)
            {
                return NotFound();
            }

            var foodlog = await _context.FoodLogs.FirstOrDefaultAsync(m => m.Id == id);
            if (foodlog == null)
            {
                return NotFound();
            }
            FoodLog = foodlog;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FoodLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodLogExists(FoodLog.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FoodLogExists(int id)
        {
            return _context.FoodLogs.Any(e => e.Id == id);
        }
    }
}
