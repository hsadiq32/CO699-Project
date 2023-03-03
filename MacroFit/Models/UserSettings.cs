using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class UserSettings  // Singular name for entity class 
    {
        public int Id { get; set; }

        [Display(Name = "Dark Mode Toggle")]
        public bool DarkModeToggle { get; set; } = true;

        [Display(Name = "Dashboard View")]
        public DashboardView DashboardView { get; set; } = DashboardView.View1;

        [Display(Name = "Auto Goal")]
        public bool AutoGoal { get; set; } = true;

        [Display(Name = "Metric Toggle")]
        public bool MetricToggle { get; set; } = true;

        [Required(ErrorMessage = "Activity Level is required.")]
        [Display(Name = "Activity Level")]
        public ActivityLevel ActivityLevel { get; set; }

        [Required(ErrorMessage = "Goal Type is required.")]
        [Display(Name = "Goal Type")]
        public GoalType GoalType { get; set; }

        [Required(ErrorMessage = "Calorie Goal is required.")]
        [Range(100, 20000, ErrorMessage = "Calorie Goal must be between 100 and 20000.")]
        [Display(Name = "Calorie Goal")]
        public int CalorieGoal { get; set; }

        [Required(ErrorMessage = "Carbohydrates percentage is required.")]
        [RegularExpression(@"^[0-9]{1,2}(\.[0-9]{1,2})?$", ErrorMessage = "Carbohydrates percentage must be a number with up to 2 decimal places.")]
        [Range(0, 100, ErrorMessage = "Carbohydrates percentage must be between 0 and 100.")]
        [Display(Name = "Carbohydrates Percentage")]
        public double CarbohydratesPercentage { get; set; }

        [Required(ErrorMessage = "Protein percentage is required.")]
        [RegularExpression(@"^[0-9]{1,2}(\.[0-9]{1,2})?$", ErrorMessage = "Protein percentage must be a number with up to 2 decimal places.")]
        [Range(0, 100, ErrorMessage = "Protein percentage must be between 0 and 100.")]
        [Display(Name = "Protein Percentage")]
        public double ProteinPercentage { get; set; }

        [Required(ErrorMessage = "Fat percentage is required.")]
        [RegularExpression(@"^[0-9]{1,2}(\.[0-9]{1,2})?$", ErrorMessage = "Fat percentage must be a number with up to 2 decimal places.")]
        [Range(0, 100, ErrorMessage = "Fat percentage must be between 0 and 100.")]
        [Display(Name = "Fat Percentage")]
        public double FatPercentage { get; set; }
    }

    public enum DashboardView
    {
        [Display(Name = "View 1")]
        View1,
        [Display(Name = "View 2")]
        View2,
        [Display(Name = "View 3")]
        View3
    }

    public enum ActivityLevel
    {
        Sedentary,
        Light,
        Moderate,
        Active,
        VeryActive
    }

    public enum GoalType
    {
        [Display(Name = "Lose Weight")]
        LoseWeight,
        [Display(Name = "Maintain Weight")]
        MaintainWeight,
        [Display(Name = "Gain Weight")]
        GainWeight
    }
}
