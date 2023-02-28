using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class FoodUnit
    {
        public int Id { get; set; } // PascalCase for property names 

        [Required(ErrorMessage = "The Name field is required.")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The Name field cannot exceed {1} characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Symbol Name field is required.")]
        [Display(Name = "Symbol")]
        [StringLength(10, ErrorMessage = "The Symbol Name field cannot exceed {1} characters.")]
        public string SymbolName { get; set; }

        [Display(Name = "Grams Conversion")]
        [Range(0.01, 10000.0, ErrorMessage = "The Grams Conversion field must be between {1}g and {2}g.")]
        public double GramsConversion { get; set; } = 0;
    }

}
