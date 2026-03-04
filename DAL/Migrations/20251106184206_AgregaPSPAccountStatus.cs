using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AgregaPSPAccountStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoCuentaPSPId",
                table: "PSPAccounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PSPAccountStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Aceptado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSPAccountStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PSPAccounts_EstadoCuentaPSPId",
                table: "PSPAccounts",
                column: "EstadoCuentaPSPId");

            migrationBuilder.AddForeignKey(
                name: "FK_PSPAccounts_PSPAccountStatus_EstadoCuentaPSPId",
                table: "PSPAccounts",
                column: "EstadoCuentaPSPId",
                principalTable: "PSPAccountStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PSPAccounts_PSPAccountStatus_EstadoCuentaPSPId",
                table: "PSPAccounts");

            migrationBuilder.DropTable(
                name: "PSPAccountStatus");

            migrationBuilder.DropIndex(
                name: "IX_PSPAccounts_EstadoCuentaPSPId",
                table: "PSPAccounts");

            migrationBuilder.DropColumn(
                name: "EstadoCuentaPSPId",
                table: "PSPAccounts");
        }
    }
}
