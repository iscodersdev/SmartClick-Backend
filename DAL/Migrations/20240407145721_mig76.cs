using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class mig76 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Foto1",
                table: "Productos",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto2",
                table: "Productos",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto3",
                table: "Productos",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto4",
                table: "Productos",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto5",
                table: "Productos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto1",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Foto2",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Foto3",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Foto4",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Foto5",
                table: "Productos");
        }
    }
}
