using MacroFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MacroFit.Data
{
    public class MacroFitContext : IdentityDbContext<IdentityUser>
    {
        public MacroFitContext(DbContextOptions<MacroFitContext> options) : base(options)
        {
        }

        public DbSet<User> Accounts { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodLog> FoodLogs { get; set; }
        public DbSet<FoodUnit> FoodUnits { get; set; }
        public DbSet<UserExercise> UserExercises { get; set; }
        public DbSet<UserProgress> UserProgress { get; set; }
        public DbSet<UserFeedback> UserFeedback { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<StudyTopic> StudyTopics { get; set; }
        public DbSet<Study> Studies { get; set; }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // User
        //    modelBuilder.Entity<Account>().ToTable("Account");
        //    modelBuilder.Entity<AccountProgress>().ToTable("AccountProgress");
        //    modelBuilder.Entity<AccountFeedback>().ToTable("AccountFeedback");
        //    modelBuilder.Entity<AccountSettings>().ToTable("AccountSettings");
        //    modelBuilder.Entity<AccountExercise>().ToTable("AccountExercise");

        //    // Food
        //    modelBuilder.Entity<Food>().ToTable("Food");
        //    modelBuilder.Entity<FoodLog>().ToTable("FoodLog");
        //    modelBuilder.Entity<FoodUnit>().ToTable("FoodUnit");

        //    // Studies
        //    modelBuilder.Entity<StudyTopic>().ToTable("StudyTopic");
        //    modelBuilder.Entity<Study>().ToTable("Study");
        //}

    }



}
