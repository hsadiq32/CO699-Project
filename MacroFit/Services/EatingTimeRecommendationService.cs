using MacroFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MacroFit.Services
{
    public class EatingTimeRecommendationService
    {
        private const int MinDataPoints = 10; // Minimum number of data points to make recommendations
        private const int DefaultTimezoneCount = 3; // Default number of timezones to recommend

        public List<DateTime> GetOptimalEatingTimes(IEnumerable<FoodLog> userFoodLogs)
        {
            if (userFoodLogs.Count() < MinDataPoints)
            {
                return GetDefaultEatingTimes();
            }

            // Group food logs by hunger level
            var logsByHungerLevel = userFoodLogs.GroupBy(log => log.HungerLevel)
                .OrderByDescending(group => group.Key);

            var recommendedTimes = new List<DateTime>();

            foreach (var group in logsByHungerLevel)
            {
                var averageTimes = group
                    .GroupBy(log => log.Type)
                    .Select(g => new
                    {
                        MealType = g.Key,
                        AverageTime = TimeSpan.FromTicks((long)g.Average(x => x.DateTime.TimeOfDay.Ticks))
                    })
                    .OrderBy(at => at.AverageTime)
                    .ToList();

                foreach (var avgTime in averageTimes)
                {
                    var time = DateTime.Today.Add(avgTime.AverageTime);
                    recommendedTimes.Add(time);
                }

                if (recommendedTimes.Count >= DefaultTimezoneCount)
                {
                    break;
                }
            }

            return recommendedTimes.OrderBy(t => t.TimeOfDay).Take(DefaultTimezoneCount).ToList();
        }

        private List<DateTime> GetDefaultEatingTimes()
        {
            return new List<DateTime>
            {
                DateTime.Today.AddHours(8),  // Default breakfast time
                DateTime.Today.AddHours(12), // Default lunch time
                DateTime.Today.AddHours(18)  // Default dinner time
            };
        }
    }
}
