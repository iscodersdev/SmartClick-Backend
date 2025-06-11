using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class mig83 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LineaPrestamoId",
                table: "UatBot",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Canal",
                table: "Prestamos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineaPrestamoId",
                table: "UatBot");

            migrationBuilder.DropColumn(
                name: "Canal",
                table: "Prestamos");
        }
    }
}
