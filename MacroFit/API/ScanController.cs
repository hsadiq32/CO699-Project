using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using MacroFit.Data;
using MacroFit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MacroFit.API
{
    [Route("api/scan")]
    [ApiController]
    public class ScanController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly MacroFitContext _context;
        private FoodUnit FoodUnit;

        public ScanController(HttpClient httpClient, MacroFitContext context)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _context = context;
        }

        // GET: api/scan/{barcode}
        [HttpGet("{code}")]
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
                var url = $"https://world.openfoodfacts.org/api/v0/product/{code}";
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
                            Name = product.ProductData.product_name,
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
        }

        private class ProductData
        {
            public string brands { get; set; }

            public string brands_imported { get; set; }

            public string product_name { get; set; }

            public int product_quantity { get; set; }
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
