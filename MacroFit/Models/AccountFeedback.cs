using Faker;
using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class AccountFeedback  // Singular name for entity class 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date Time is required.")]
        [Display(Name = "Date Time")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Feedback is required.")]
        [StringLength(500, ErrorMessage = "Feedback must be at most 500 characters long.")]
        public string Feedback { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        // Navigation properties

        [Required(ErrorMessage = "Account is required.")]
        [Display(Name = "Account")]
        public Account Account { get; set; }
    }
}
