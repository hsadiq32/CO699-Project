using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class StudyTopic
    {
        public int Id { get; set; } // PascalCase for property names 

        [Required(ErrorMessage = "The name field is required")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The name field must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description field is required")]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "The description field cannot exceed {1} characters.")]
        public string Description { get; set; }
    }
}
