using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacroFit.Migrations
{
    public partial class userdata_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AccountSettings_AccountSettingsId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AccountExercises");

            migrationBuilder.DropTable(
                name: "AccountFeedback");

            migrationBuilder.DropTable(
                name: "AccountProgress");

            migrationBuilder.DropTable(
                name: "AccountSettings");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AccountSettingsId",
                table: "AspNetUsers",
                newName: "UserSettingsId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AccountSettingsId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_UserSettingsId");

            migrationBuilder.CreateTable(
                name: "UserExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    Intensity = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExercises_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFeedback_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    BodyFatPercentage = table.Column<double>(type: "float", nullable: false),
                    ProgressImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProgress_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DarkModeToggle = table.Column<bool>(type: "bit", nullable: false),
                    DashboardView = table.Column<int>(type: "int", nullable: false),
                    AutoGoal = table.Column<bool>(type: "bit", nullable: false),
                    MetricToggle = table.Column<bool>(type: "bit", nullable: false),
                    ActivityLevel = table.Column<int>(type: "int", nullable: false),
                    GoalType = table.Column<int>(type: "int", nullable: false),
                    CalorieGoal = table.Column<int>(type: "int", nullable: false),
                    CarbohydratesPercentage = table.Column<double>(type: "float", nullable: false),
                    ProteinPercentage = table.Column<double>(type: "float", nullable: false),
                    FatPercentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserExercises_AccountId",
                table: "UserExercises",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedback_AccountId",
                table: "UserFeedback",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_AccountId",
                table: "UserProgress",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserSettings_UserSettingsId",
                table: "AspNetUsers",
                column: "UserSettingsId",
                principalTable: "UserSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserSettings_UserSettingsId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserExercises");

            migrationBuilder.DropTable(
                name: "UserFeedback");

            migrationBuilder.DropTable(
                name: "UserProgress");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.RenameColumn(
                name: "UserSettingsId",
                table: "AspNetUsers",
                newName: "AccountSettingsId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_UserSettingsId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AccountSettingsId");

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    Intensity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountExercises_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountFeedback_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BodyAge = table.Column<int>(type: "int", nullable: false),
                    BodyFatPercentage = table.Column<double>(type: "float", nullable: false),
                    BoneMass = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MuscleMass = table.Column<double>(type: "float", nullable: false),
                    WaterPercentage = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountProgress_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityLevel = table.Column<int>(type: "int", nullable: false),
                    AutoGoal = table.Column<bool>(type: "bit", nullable: false),
                    CalorieGoal = table.Column<int>(type: "int", nullable: false),
                    CarbohydratesPercentage = table.Column<double>(type: "float", nullable: false),
                    DarkModeToggle = table.Column<bool>(type: "bit", nullable: false),
                    DashboardView = table.Column<int>(type: "int", nullable: false),
                    FatPercentage = table.Column<double>(type: "float", nullable: false),
                    GoalType = table.Column<int>(type: "int", nullable: false),
                    MetricToggle = table.Column<bool>(type: "bit", nullable: false),
                    ProteinPercentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountExercises_AccountId",
                table: "AccountExercises",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFeedback_AccountId",
                table: "AccountFeedback",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountProgress_AccountId",
                table: "AccountProgress",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AccountSettings_AccountSettingsId",
                table: "AspNetUsers",
                column: "AccountSettingsId",
                principalTable: "AccountSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
