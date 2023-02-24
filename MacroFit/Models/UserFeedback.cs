namespace MacroFit.Models
{
    public class UserFeedback  // Singular name for entity class 
    {
        public int Id { get; set; }  // PascalCase for property names
        public int UserId { get; set; }  // UserId instead of User ID as foreign key name
        public DateTime Date { get; set; }  // DateTime type for date
        public string Feedback { get; set; }
        public int Rating { get; set; }

        // Navigation properties

        public User User { get; set; }  // Singular name for navigation property 

    }
}
