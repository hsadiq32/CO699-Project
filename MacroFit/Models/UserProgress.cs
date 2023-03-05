using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class UserProgress
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Display(Name = "Weight")]
        [Range(1, 500, ErrorMessage = "Weight must be between 1 and 500 kilograms")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Height is required")]
        [Display(Name = "Height")]
        [Range(0.5, 3, ErrorMessage = "Height must be between 0.5 and 3.0 meters")] // This attribute validates that the value is within a specified range and specifies the error message
        public double Height { get; set; } // Double type for decimal numbers

        [RegularExpression(@"^[0-9]{1,2}(\.[0-9]{1,2})?$", ErrorMessage = "Body fat percentage must be a number with up to 2 decimal places.")]
        [Range(0, 100, ErrorMessage = "Body fat percentage must be between 0 and 100.")]
        [Display(Name = "Body Fat Percentage")]
        public double BodyFatPercentage { get; set; }

        [Display(Name = "Progress Image")]
        public string ProgressImage { get; set; }


        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        // Navigation properties

        [Required(ErrorMessage = "Account is required.")]
        [Display(Name = "Account")]
        public User Account { get; set; }
    }

    //[Display(Name = "Image")]
    //[MaxFileSize(10 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 10MB.")]
    //[AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "Only JPG and PNG file types are allowed.")]
    //public IFormFile ImageFile { get; set; }


}
