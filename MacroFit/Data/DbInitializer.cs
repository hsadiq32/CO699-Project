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

            // Seed food units
            if (!context.FoodUnits.Any())
            {
                var units = new List<FoodUnit>
                {
                    new FoodUnit
                    {
                        Name = "Grams",
                        SymbolName = "g",
                        GramsConversion = 1
                    },
                    new FoodUnit
                    {
                        Name = "Kilograms",
                        SymbolName = "kg",
                        GramsConversion = 1000
                    },
                    new FoodUnit
                    {
                        Name = "Milligrams",
                        SymbolName = "mg",
                        GramsConversion = 0.1
                    },
                    new FoodUnit
                    {
                        Name = "Litres",
                        SymbolName = "L",
                        GramsConversion = 1000
                    },
                    new FoodUnit
                    {
                        Name = "Millilitres",
                        SymbolName = "ml",
                        GramsConversion = 1
                    },
                    new FoodUnit
                    {
                        Name = "Milk Litres",
                        SymbolName = "L",
                        GramsConversion = 1000
                    },
                    new FoodUnit
                    {
                        Name = "Ounces",
                        SymbolName = "oz",
                        GramsConversion = 28.3495
                    },
                    new FoodUnit
                    {
                        Name = "Pounds",
                        SymbolName = "lb",
                        GramsConversion = 453.592
                    },
                    new FoodUnit
                    {
                        Name = "Tablespoon",
                        SymbolName = "tbsp",
                        GramsConversion = 14.7868
                    },
                    new FoodUnit
                    {
                        Name = "Teaspoon",
                        SymbolName = "tsp",
                        GramsConversion = 4.92892
                    }
                };
                context.FoodUnits.AddRange(units);
                context.SaveChanges();
            }
        }
    }
}
