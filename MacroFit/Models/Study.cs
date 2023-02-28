using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class Study
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The reference number is required.")]
        [Display(Name = "Reference Number")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "The reference number can only contain alphanumeric characters.")]
        public string ReferenceNumber { get; set; }

        [Required(ErrorMessage = "The title is required.")]
        [Display(Name = "Title")]
        [MaxLength(100, ErrorMessage = "The title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The author is required.")]
        [Display(Name = "Author")]
        [MaxLength(50, ErrorMessage = "The author name cannot exceed 50 characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The publication date is required.")]
        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage = "The abstract is required.")]
        [Display(Name = "Abstract")]
        [MaxLength(500, ErrorMessage = "The abstract cannot exceed 500 characters.")]
        public string Abstract { get; set; }

        [Required(ErrorMessage = "The link is required.")]
        [Display(Name = "Link")]
        [RegularExpression(@"^https?://", ErrorMessage = "The link must be a valid URL starting with 'http://' or 'https://'.")]
        public string Link { get; set; }

        // Navigation properties
        [Required(ErrorMessage = "The Study Topic is required.")]
        [Display(Name = "Study Topic")]
        public StudyTopic StudyTopic { get; set; }
    }
}
