using System.Collections.Generic;
using System.Linq;
using FuzzySharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;
using static ZXing.QrCode.Internal.Mode;

namespace MacroFit.API
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly MacroFitContext _context;

        public SearchController(MacroFitContext context)
        {
            _context = context;
        }
        // GET api/search/foods/query
        [HttpGet("{query}")]
        public async Task<ActionResult<IEnumerable<Food>>> SearchFoods(string query)
        {
            if (string.IsNullOrEmpty(query) || query.Length < 3)
            {
                return BadRequest("Invalid query parameter");
            }

            var foods = await _context.Foods
                .Include(f => f.FoodUnit)
                .Where(f => f.Name.Contains(query) || f.Barcode.Contains(query))
                .Take(30)
                .ToListAsync();

            if (foods.Count == 0)
            {
                return NotFound();
            }

            return foods;
        }

    }
}
