using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class mig81 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "UatBot",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadCuotas",
                table: "UatBot",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "FirmaOlografica",
                table: "UatBot",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FotoDNIAnverso",
                table: "UatBot",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FotoDNIReverso",
                table: "UatBot",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FotoSosteniendoDNI",
                table: "UatBot",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ImporteSolicitado",
                table: "UatBot",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoCuota",
                table: "UatBot",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "UatBot",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "UatBot");

            migrationBuilder.DropColumn(
                name: "CantidadCuotas",
                table: "UatBot");

            migrationBuilder.DropColumn(
                name: "FirmaOlografica",
                table: "UatBot");

            migrationBuilder.DropColumn(
                name: "FotoDNIAnverso",
                table: "UatBot");

            migrationBuilder.DropColumn(
                name: "FotoDNIReverso",
                table: "UatBot");

            migrationBuilder.DropColumn(
                name: "FotoSosteniendoDNI",
                table: "UatBot");

            migrationBuilder.DropColumn(
                name: "ImporteSolicitado",
                table: "UatBot");

            migrationBuilder.DropColumn(
                name: "MontoCuota",
                table: "UatBot");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "UatBot");
        }
    }
}
