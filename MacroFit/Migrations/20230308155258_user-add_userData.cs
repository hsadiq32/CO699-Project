using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacroFit.Migrations
{
    public partial class useradd_userData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserData",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserData",
                table: "AspNetUsers");
        }
    }
}
