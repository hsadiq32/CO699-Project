using MacroFit.Models;
using Microsoft.EntityFrameworkCore;

namespace MacroFit.Data
{
    public class MacroFitContext : DbContext
    {
        public MacroFitContext(DbContextOptions<MacroFitContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodLog> FoodLogs { get; set; }
        public DbSet<FoodUnit> FoodUnits { get; set; }
        public DbSet<AccountExercise> Exercises { get; set; }
        public DbSet<AccountProgress> UsersProgress { get; set; }
        public DbSet<AccountFeedback> UsersFeedback { get; set; }
        public DbSet<AccountSettings> UsersSettings { get; set; }
        public DbSet<StudyTopic> StudyTopics { get; set; }
        public DbSet<Study> Studies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<AccountProgress>().ToTable("AccountProgress");
            modelBuilder.Entity<AccountFeedback>().ToTable("AccountFeedback");
            modelBuilder.Entity<AccountSettings>().ToTable("AccountSettings");
            modelBuilder.Entity<AccountExercise>().ToTable("AccountExercise");

            // Food
            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<FoodLog>().ToTable("FoodLog");
            modelBuilder.Entity<FoodUnit>().ToTable("FoodUnit");

            // Studies
            modelBuilder.Entity<StudyTopic>().ToTable("StudyTopic");
            modelBuilder.Entity<Study>().ToTable("Study");
        }

    }
}
