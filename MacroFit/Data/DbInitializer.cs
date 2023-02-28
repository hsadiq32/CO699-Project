using MacroFit.Models;
using Microsoft.EntityFrameworkCore;
using Faker;

namespace MacroFit.Data
{
    public class DbInitializer
    {
        public static void Initialize(MacroFitContext context)
        {
            if (context.Users.Any())
            {
                
            }
        }
    }

}
