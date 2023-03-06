using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class Food
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters long.")]
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Barcode must be alphanumeric.")]
        [Display(Name = "Barcode")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "Serving size is required.")]
        [Display(Name = "Serving Size")]
        public double ServingSize { get; set; }

        [Required(ErrorMessage = "Calories are required.")]
        [Display(Name = "Calories")]
        public double Calories { get; set; }

        [Required(ErrorMessage = "Protein is required.")]
        [Display(Name = "Protein")]
        public double Protein { get; set; }

        [Required(ErrorMessage = "Carbohydrates are required.")]
        [Display(Name = "Carbohydrates")]
        public double Carbohydrates { get; set; }

        [Required(ErrorMessage = "Fat is required.")]
        [Display(Name = "Fat")]
        public double Fat { get; set; }

        [Required(ErrorMessage = "Saturated fat is required.")]
        [Display(Name = "Saturated Fat")]
        public double FatSat { get; set; }

        [Required(ErrorMessage = "Fibre is required.")]
        [Display(Name = "Fibre")]
        public double Fibre { get; set; }

        [Required(ErrorMessage = "Sugar is required.")]
        [Display(Name = "Sugar")]
        public double Sugar { get; set; }

        [Required(ErrorMessage = "Sodium is required.")]
        [Display(Name = "Sodium")]
        public double Sodium { get; set; }

        // Navigation properties

        [Required(ErrorMessage = "Food unit is required.")]
        [Display(Name = "Food Unit")]
        public FoodUnit FoodUnit { get; set; }
    }

}
