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
        public DbSet<FoodLog> Meals { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<UserProgress> Progresses { get; set; }
        public DbSet<UserFeedback> UsersFeedback { get; set; }
        public DbSet<UserSettings> AppSettings { get; set; }
        public DbSet<StudyTopic> StudyTopics { get; set; }
        public DbSet<Study> Studies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<FoodLog>().ToTable("Meal");
            modelBuilder.Entity<Exercise>().ToTable("Exercise");
            modelBuilder.Entity<UserProgress>().ToTable("Progress");
            modelBuilder.Entity<UserFeedback>().ToTable("UserFeedback");
            modelBuilder.Entity<UserSettings>().ToTable("AppSetting");
            modelBuilder.Entity<StudyTopic>().ToTable("StudyTopic");
            modelBuilder.Entity<Study>().ToTable("Study");
        }

    }
}
