using MacroFit.Models;
using Microsoft.EntityFrameworkCore;

namespace MacroFit.Data
{
    public class MacroFitContext : DbContext
    {
        public MacroFitContext(DbContextOptions<MacroFitContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodLog> FoodLogs { get; set; }
        public DbSet<FoodUnit> FoodUnits { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<UserProgress> UsersProgress { get; set; }
        public DbSet<UserFeedback> UsersFeedback { get; set; }
        public DbSet<UserSettings> UsersSettings { get; set; }
        public DbSet<StudyTopic> StudyTopics { get; set; }
        public DbSet<Study> Studies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserProgress>().ToTable("UserProgress");
            modelBuilder.Entity<UserFeedback>().ToTable("UserFeedback");
            modelBuilder.Entity<UserSettings>().ToTable("UserSettings");

            // Food
            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<FoodLog>().ToTable("FoodLog");
            modelBuilder.Entity<FoodUnit>().ToTable("FoodUnit");

            // Fitness
            modelBuilder.Entity<Exercise>().ToTable("Exercise");

            // Studies
            modelBuilder.Entity<StudyTopic>().ToTable("StudyTopic");
            modelBuilder.Entity<Study>().ToTable("Study");
        }

    }
}
