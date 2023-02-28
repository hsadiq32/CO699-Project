using System;

namespace MacroFit.Models
{
    public class User // Singular name for entity class
    {
        public int Id { get; set; } // PascalCase for property names
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; } // DateTime type for date
        public Gender Gender { get; set; }
        public double Height { get; set; } // Double type for decimal numbers
        public double Weight { get; set; }

        // Navigation properties
        public UserSettings UserSettings { get; set; }  // Singular name for navigation property 

    }

    public enum Gender
    {
        Male,
        Female
    }
}
