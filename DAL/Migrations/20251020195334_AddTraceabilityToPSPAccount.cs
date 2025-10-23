using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddTraceabilityToPSPAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EntityId",
                table: "PSPAccounts",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EncryptedUserToken",
                table: "PSPAccounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CVU",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EncryptedPassword",
                table: "PSPAccounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastC1ResponseJson",
                table: "PSPAccounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastC7ResponseJson",
                table: "PSPAccounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStatusCheck",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PSPAccounts_ClienteId",
                table: "PSPAccounts",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PSPAccounts_UsuarioId1",
                table: "PSPAccounts",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PSPAccounts_Clientes_ClienteId",
                table: "PSPAccounts",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PSPAccounts_AspNetUsers_UsuarioId1",
                table: "PSPAccounts",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PSPAccounts_Clientes_ClienteId",
                table: "PSPAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PSPAccounts_AspNetUsers_UsuarioId1",
                table: "PSPAccounts");

            migrationBuilder.DropIndex(
                name: "IX_PSPAccounts_ClienteId",
                table: "PSPAccounts");

            migrationBuilder.DropIndex(
                name: "IX_PSPAccounts_UsuarioId1",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "CVU",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "EncryptedPassword",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "LastC1ResponseJson",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "LastC7ResponseJson",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "LastStatusCheck",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "PSPAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "EntityId",
                table: "PSPAccounts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EncryptedUserToken",
                table: "PSPAccounts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
