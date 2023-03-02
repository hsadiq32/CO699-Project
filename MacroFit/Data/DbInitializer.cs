using MacroFit.Models;
using Microsoft.EntityFrameworkCore;

namespace MacroFit.Data
{
    public class DbInitializer
    {
        public static void Initialize(MacroFitContext context)
        {
            // Seed users
            if (!context.Accounts.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        DateOfBirth = new DateTime(1985, 1, 1),
                        Gender = Gender.Male,
                        Height = 1.8,
                        Weight = 75,
                        UserSettings = new UserSettings
                        {
                            CalorieGoal = 2000,
                            GoalType = GoalType.MaintainWeight,
                            ActivityLevel = ActivityLevel.VeryActive,
                            ProteinPercentage = 30,
                            CarbohydratesPercentage = 50,
                            FatPercentage = 20
                        }
                    },
                    new User
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        Email = "jane.doe@example.com",
                        DateOfBirth = new DateTime(1990, 2, 2),
                        Gender = Gender.Female,
                        Height = 1.6,
                        Weight = 60,
                        UserSettings = new UserSettings
                        {
                            CalorieGoal = 1800,
                            DarkModeToggle = false,
                            ActivityLevel = ActivityLevel.Active,
                            GoalType = GoalType.LoseWeight,
                            ProteinPercentage = 25,
                            CarbohydratesPercentage = 55,
                            FatPercentage = 20
                        }
                    },
                    new User
                    {
                        FirstName = "Bob",
                        LastName = "Smith",
                        Email = "bob.smith@example.com",
                        DateOfBirth = new DateTime(1975, 3, 3),
                        Gender = Gender.Male,
                        Height = 1.85,
                        Weight = 90,
                        UserSettings = new UserSettings
                        {
                            CalorieGoal = 2200,
                            DarkModeToggle= false,
                            DashboardView = DashboardView.View2,
                            ActivityLevel = ActivityLevel.Light,
                            GoalType = GoalType.GainWeight,
                            ProteinPercentage = 35,
                            CarbohydratesPercentage = 45,
                            FatPercentage = 20
                        }
                    }
                };
                context.Accounts.AddRange(users);
                context.SaveChanges();
            }
        }
    }

}
