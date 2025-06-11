using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class mig75 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconoAPP",
                table: "Rubros",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VerEnAPP",
                table: "Rubros",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconoAPP",
                table: "Rubros");

            migrationBuilder.DropColumn(
                name: "VerEnAPP",
                table: "Rubros");
        }
    }
}
