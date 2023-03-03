﻿using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace MacroFit.Models
{
    public class User : IdentityUser // Singular name for entity class
    {

        [Required(ErrorMessage = "First name is required")] // This attribute indicates that the property is required and specifies the error message
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")] // This attribute limits the length of the string and specifies the error message
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First name can only contain alphabetical characters")] // This attribute validates that the string matches a regular expression and specifies the error message
        public string FirstName { get; set; } // Split name to first and last name variables

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last name can only contain alphabetical characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } // DateTime type for date

        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Height is required")]
        [Display(Name = "Height")]
        [Range(0.5, 3, ErrorMessage = "Height must be between 0.5 and 3.0 meters")] // This attribute validates that the value is within a specified range and specifies the error message
        public double Height { get; set; } // Double type for decimal numbers

        [Required(ErrorMessage = "Weight is required")]
        [Display(Name = "Weight")]
        [Range(1, 500, ErrorMessage = "Weight must be between 1 and 500 kilograms")]
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