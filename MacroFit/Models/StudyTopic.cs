namespace MacroFit.Models
{
    public class StudyTopic  // Singular name for entity class 
    {
        public int Id { get; set; }  // PascalCase for property names
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation properties

        public ICollection<Study> Studies { get; set; }  // ICollection<T> type for navigation properties 

    }
}
