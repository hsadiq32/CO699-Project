using Faker;
using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class AccountProgress
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(0, 1000, ErrorMessage = "Weight must be between 0 and 1000.")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Body fat percentage is required.")]
        [RegularExpression(@"^[0-9]{1,2}(\.[0-9]{1,2})?$", ErrorMessage = "Body fat percentage must be a number with up to 2 decimal places.")]
        [Range(0, 100, ErrorMessage = "Body fat percentage must be between 0 and 100.")]
        [Display(Name = "Body Fat Percentage")]
        public double BodyFatPercentage { get; set; }

        [Required(ErrorMessage = "Muscle mass is required.")]
        [Range(0, 1000, ErrorMessage = "Muscle mass must be between 0 and 1000.")]
        public double MuscleMass { get; set; }

        [Required(ErrorMessage = "Water percentage is required.")]
        [RegularExpression(@"^[0-9]{1,2}(\.[0-9]{1,2})?$", ErrorMessage = "Water percentage must be a number with up to 2 decimal places.")]
        [Range(0, 100, ErrorMessage = "Water percentage must be between 0 and 100.")]
        [Display(Name = "Water Percentage")]
        public double WaterPercentage { get; set; }

        [Required(ErrorMessage = "Bone mass is required.")]
        [Range(0, 1000, ErrorMessage = "Bone mass must be between 0 and 1000.")]
        public double BoneMass { get; set; }

        [Required(ErrorMessage = "Body age is required.")]
        [Range(1, 120, ErrorMessage = "Body age must be between 1 and 120.")]
        [Display(Name = "Body Age")]
        public int BodyAge { get; set; }

        // Navigation properties

        [Required(ErrorMessage = "Account is required.")]
        [Display(Name = "Account")]
        public Account Account { get; set; }
    }

}
