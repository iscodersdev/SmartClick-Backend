using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AlignPspAccountModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PSPAccounts_AspNetUsers_UsuarioId1",
                table: "PSPAccounts");

            migrationBuilder.DropIndex(
                name: "IX_PSPAccounts_UsuarioId1",
                table: "PSPAccounts");

            migrationBuilder.RenameColumn(
                name: "UsuarioId1",
                table: "PSPAccounts",
                newName: "TributaryIdentifierType");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "PSPAccounts",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TributaryIdentifierType",
                table: "PSPAccounts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountTypeId",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CVU_CBUAlias",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyDescription",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencySymbol",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyTypeId",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DeleteAccountSolicitude",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntityStatus",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntityStatusDescription",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PSPAccounts_UsuarioId",
                table: "PSPAccounts",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PSPAccounts_AspNetUsers_UsuarioId",
                table: "PSPAccounts",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PSPAccounts_AspNetUsers_UsuarioId",
                table: "PSPAccounts");

            migrationBuilder.DropIndex(
                name: "IX_PSPAccounts_UsuarioId",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "AccountTypeId",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "CVU_CBUAlias",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "CurrencyDescription",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "CurrencySymbol",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "CurrencyTypeId",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "DeleteAccountSolicitude",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "EntityStatusDescription",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "StatusDescription",
                table: "PSPAccounts");

            migrationBuilder.RenameColumn(
                name: "TributaryIdentifierType",
                table: "PSPAccounts",
                newName: "UsuarioId1");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "PSPAccounts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId1",
                table: "PSPAccounts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PSPAccounts_UsuarioId1",
                table: "PSPAccounts",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PSPAccounts_AspNetUsers_UsuarioId1",
                table: "PSPAccounts",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
