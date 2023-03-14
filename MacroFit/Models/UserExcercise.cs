using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class UserExercise
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Exercise type is required.")]
        [Display(Name = "Exercise Type")]
        public ExerciseType Type { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "Duration must be a number with up to 2 decimal places.")]
        [Range(0, 24, ErrorMessage = "Duration must be between 0 and 24.")]
        [Display(Name = "Duration (hours)")]
        public double Duration { get; set; }

        [Required(ErrorMessage = "Exercise intensity is required.")]
        [Display(Name = "Exercise Intensity")]
        public ExerciseIntensity Intensity { get; set; }

        // Navigation properties

        [Required(ErrorMessage = "Account is required.")]
        [Display(Name = "Account")]
        public User Account { get; set; }
    }

    public enum ExerciseType
    {
        [Display(Name = "Cardiovascular")]
        Cardiovascular,
        [Display(Name = "Strength Training")]
        StrengthTraining
    }

    public enum ExerciseIntensity
    {
        [Display(Name = "Low Intensity")]
        LowIntensity,
        [Display(Name = "Moderate Intensity")]
        ModerateIntensity,
        [Display(Name = "High Intensity")]
        HighIntensity
    }
}
