using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MacroFit.Models
{

    public class FoodResponse
        {
            public int Status { get; set; } = 0;
            public string Message { get; set; } = "Barcode Found";
            public string Barcode { get; set; }
            public FoodResponseDetails FoodResponseDetails { get; set; }
        }

        public class FoodResponseDetails
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Carbohydrates { get; set; }
            public double Protein { get; set; }
            public double Fat { get; set; }
        }

        public class ProductResponse
        {
            public int status { get; set; }
            [JsonProperty("product")]
            public ProductData ProductData { get; set; }
        }

        public class ProductData
        {
            public string brands { get; set; }

            public string product_name { get; set; }

            public int product_quantity { get; set; }
            public string quantity { get; set; }

            [JsonProperty("nutriments")]
            public Nutriments Nutriments { get; set; }

        public static string ExtractUnit(string unitString)
        {
            if (unitString == null)
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

        public class Nutriments
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
