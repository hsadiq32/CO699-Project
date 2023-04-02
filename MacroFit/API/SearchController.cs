using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MacroFit.Data;
using MacroFit.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MacroFit.API
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly MacroFitContext _context;
        private readonly HttpClient _httpClient;
        private FoodUnit FoodUnit;

        public SearchController(HttpClient httpClient, MacroFitContext context)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _context = context;
        }


        // GET api/search/list/{query}
        [HttpGet("list/{query}")]
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

            if (foods.Count < 30)
            {
                string apiUrl = $"https://uk.openfoodfacts.org/cgi/search.pl?search_terms={query}&search_simple=1&action=process&json=1&page_size=50";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var productResponse = JsonConvert.DeserializeObject<ProductResponse>(responseContent);

                        foreach (var product in productResponse.ProductDataList)
                        {
                            if (!foods.Any(f => f.Barcode == product.code))
                            {

                                string customUnit = ExtractUnit(product.quantity);
                                FoodUnit = await _context.FoodUnits.FirstOrDefaultAsync(m => m.SymbolName.ToUpper() == customUnit.ToUpper());
                                if (FoodUnit == null)
                                {
                                    FoodUnit = await _context.FoodUnits.FirstOrDefaultAsync(m => m.Id == 1);
                                }
                                Food newFood = new Food
                                {
                                    Barcode = product.code,
                                    Name = FoodNameParser(product.brands, product.product_name),
                                    ServingSize = product.product_quantity,
                                    Calories = product.Nutriments.energy_kcal_100g,
                                    Protein = product.Nutriments.proteins_100g,
                                    Carbohydrates = product .Nutriments.carbohydrates_100g,
                                    Fat = product.Nutriments.fat_100g,
                                    FatSat = product.Nutriments.saturated_fat_100g,
                                    Fibre = product.Nutriments.fiber_100g,
                                    Sugar = product.Nutriments.sugars_100g,
                                    Sodium = product.Nutriments.sodium_100g,
                                    FoodUnit = FoodUnit
                                };
                                _context.Foods.Add(newFood);
                                foods.Add(newFood);
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (foods.Count == 0)
            {
                return NotFound();
            }

            return foods.Take(30).ToList();
        }

        // GET: api/search/barcode/{barcode}
        [HttpGet("barcode/{code}")]
        public async Task<IActionResult> GetAsync(string code)
        {
            // Check if the given barcode exists in the Food database
            var existingFood = await _context.Foods
                .Include(f => f.FoodUnit)
                .FirstOrDefaultAsync(f => f.Barcode == code);

            if (existingFood != null)
            {
                // If the barcode is found in the Food database, return the corresponding Food object.
                return Ok(new FoodResponse 
                {
                    Status = 200,
                    Barcode = existingFood.Barcode,
                    FoodResponseDetails = new FoodResponseDetails
                    {
                        Id = existingFood.Id,
                        Name = existingFood.Name,
                        Carbohydrates = existingFood.Carbohydrates,
                        Protein = existingFood.Protein,
                        Fat = existingFood.Fat,
                    }
                });
            }
            else
            {
                // If the barcode is not found in the Food database, query the open food facts api to check if the barcode is valid.
                var url = $"https://uk.openfoodfacts.org/api/v0/product/{code}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<ProductResponse>(responseContent);

                    if (product.status == 1)
                    {
                        // If the barcode is valid, add a new Food object to the Food database using LINQ queries and return the newly added Food object.
                        string customUnit = ExtractUnit(product.ProductData.quantity);
                        FoodUnit = await _context.FoodUnits.FirstOrDefaultAsync(m => m.SymbolName.ToUpper() == customUnit.ToUpper());
                        if (FoodUnit == null)
                        {
                            FoodUnit = await _context.FoodUnits.FirstOrDefaultAsync(m => m.Id == 1);
                        }

                        var newFood = new Food
                        {
                            Barcode = code,
                            Name = FoodNameParser(product.ProductData.brands, product.ProductData.product_name),
                            ServingSize = product.ProductData.product_quantity,
                            Calories = product.ProductData.Nutriments.energy_kcal_100g,
                            Protein = product.ProductData.Nutriments.proteins_100g,
                            Carbohydrates = product.ProductData.Nutriments.carbohydrates_100g,
                            Fat = product.ProductData.Nutriments.fat_100g,
                            FatSat = product.ProductData.Nutriments.saturated_fat_100g,
                            Fibre = product.ProductData.Nutriments.fiber_100g,
                            Sugar = product.ProductData.Nutriments.sugars_100g,
                            Sodium = product.ProductData.Nutriments.sodium_100g,
                            FoodUnit = FoodUnit
                        };

                        _context.Foods.Add(newFood);
                        await _context.SaveChangesAsync();
                        return Ok(new FoodResponse
                        {
                            Status = 200,
                            Barcode = newFood.Barcode,
                            FoodResponseDetails = new FoodResponseDetails
                            {
                                Id = newFood.Id,
                                Name = newFood.Name,
                                Carbohydrates = newFood.Carbohydrates,
                                Protein = newFood.Protein,
                                Fat = newFood.Fat,
                            }
                        });
                    }
                    else
                    {
                        // If the barcode is not valid, return an error message.
                        return Ok(new FoodResponse
                        {
                            Status = 400,
                            Barcode = code,
                            Message = "Barcode not Found"
                        });
                    }
                }
                else
                {
                    return Ok(new FoodResponse
                    {
                        Status = 500,
                        Barcode = code,
                        Message = $"Failed to get product status. Status code: {response.StatusCode}. Reason: {response.ReasonPhrase}",
                    });
                }
            }
        }

        public string FoodNameParser(string brand, string food)
        {
            string[] brandName;
            string[] foodName;
            if (food == null && brand == null)
            {
                return "Food Product";
            }
            else if (food == null)
            {
                brandName = brand.Contains(',') ? brand.Split(',') : new string[] { brand };
                return TrimString(brandName[0]);
            }
            else if (brand == null)
            {
                foodName = foodName = food.Contains(',') ? food.Split(',') : new string[] { food };
                return TrimString(foodName[0]);
            }
            brandName = brand.Contains(',') ? brand.Split(',') : new string[] { brand };
            foodName = foodName = food.Contains(',') ? food.Split(',') : new string[] { food };
            foreach (string name in brandName)
            {
                // Check if brand is already in foodname
                if (foodName[0].ToLower().Contains(name.ToLower()))
                {
                    return TrimString(foodName[0]);
                }
            }
            return TrimString(brandName[0].TrimStart() + " " + foodName[0].TrimStart());
        }

        public string TrimString(string inputString)
        {
            if (inputString.Length > 100)
            {
                inputString = inputString.Substring(0, 97) + "...";
            }

            return inputString;
        }

        private class FoodResponse
        {
            public int Status { get; set; } = 0;
            public string Message { get; set; } = "Barcode Found";
            public string Barcode { get; set; }
            public FoodResponseDetails FoodResponseDetails { get; set; }
        }

        private class FoodResponseDetails
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Carbohydrates { get; set; }
            public double Protein { get; set; }
            public double Fat { get; set; }
        }

        private class ProductResponse
        {
            public int status { get; set; }

            [JsonProperty("product")]
            public ProductData ProductData { get; set; }

            [JsonProperty("products")]
            public List<ProductData> ProductDataList { get; set; }
        }

        private class ProductData
        {
            public string brands { get; set; }

            public string code { get; set; }

            public string product_name { get; set; }

            public double product_quantity { get; set; }
            public string quantity { get; set; }

            [JsonProperty("nutriments")]
            public Nutriments Nutriments { get; set; }

        }

        private class Nutriments
        {
            [JsonProperty("energy-kcal_100g")]
            public double energy_kcal_100g { get; set; }
            public double proteins_100g { get; set; }
            public double carbohydrates_100g { get; set; }
            public double sugars_100g { get; set; }
            public double fat_100g { get; set; }
            public double saturated_fat_100g { get; set; }
            public double fiber_100g { get; set; }
            public double sodium_100g { get; set; }

        }

        // Function to extract unit of measurement from string
        public static string ExtractUnit(string unitString)
        {
            if(unitString == null)
            {
                return "g";
            }
            // Create a regex object with the pattern
            Regex regex = new Regex("[^0-9.]+$");

            // Match the pattern with the string parameter
            Match match = regex.Match(unitString);

            // If there is a match, return it as a trimmed string
            if (match.Success)
            {
                return match.Value.Trim();
            }

            // Otherwise, return an empty string
            else
            {
                return "g";
            }
        }
    }
}
