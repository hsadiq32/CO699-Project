using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
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

        public ScanController(HttpClient httpClient, MacroFitContext context)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _context = context;
        }


        // GET: api/scan
        [HttpGet]
        public ActionResult<string[]> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/scan/{barcode}
        [HttpGet("{code}")]
        public async Task<ActionResult<Food>> GetAsync(string code)
        {
            // Check if the given barcode exists in the Food database
            var existingFood = await _context.Foods
                .Include(f => f.FoodUnit)
                .FirstOrDefaultAsync(f => f.Barcode == code);

            if (existingFood != null)
            {
                // If the barcode is found in the Food database, return the corresponding Food object.
                return existingFood;
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
                        var newFood = new Food
                        {
                            Barcode = code,
                            Name = product.ProductData.product_name,
                            ServingSize = 1,
                            Calories = product.ProductData.Nutriments.energy_kcal_100g,
                            Protein = product.ProductData.Nutriments.proteins_100g,
                            Carbohydrates = product.ProductData.Nutriments.carbohydrates_100g,
                            Fat = product.ProductData.Nutriments.fat_100g,
                            FatSat = product.ProductData.Nutriments.saturated_fat_100g ,
                            Fibre = product.ProductData.Nutriments.fiber_100g,
                            Sugar = product.ProductData.Nutriments.sugars_100g,
                            Sodium = product.ProductData.Nutriments.sodium_100g,
                            FoodUnit = new FoodUnit 
                            { 
                                Name = "Grams",
                                SymbolName = "g",
                                GramsConversion = 100
                            } // assuming the serving size unit is always gram (g)
                        };

                        _context.Foods.Add(newFood);
                        await _context.SaveChangesAsync();

                        return newFood;
                    }
                    else
                    {
                        // If the barcode is not valid, return an error message.
                        return NotFound($"Barcode {code} is not valid.");
                    }
                }
                else
                {
                    throw new Exception($"Failed to get product status. Status code: {response.StatusCode}. Reason: {response.ReasonPhrase}");
                }
            }
        }


        // POST: api/scan
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/scan/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/scan/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
    }
}
