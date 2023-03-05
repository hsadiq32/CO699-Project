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
        [DataType(DataType.Upload)]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Only jpg or png files are allowed.")]
        [MaxFileSize(10 * 1024 * 1024, ErrorMessage = "The maximum allowed file size is 10 MB.")]
        public byte[] ProgressImage { get; set; }


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

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return ValidationResult.Success;
            }

            if (file.Length > _maxFileSize)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return ValidationResult.Success;
            }

            var extension = System.IO.Path.GetExtension(file.FileName);

            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}
