using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddPSPAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PSPAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: true),
                    PSPUserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(nullable: true),
                    EntityId = table.Column<int>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    EncryptedUserToken = table.Column<string>(nullable: true),
                    TokenExpiry = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    RequestId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    TributaryIdentifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSPAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PSPAccountFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PSPAccountId = table.Column<int>(nullable: false),
                    FileKey = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    StoragePath = table.Column<string>(nullable: true),
                    UploadedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSPAccountFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PSPAccountFiles_PSPAccounts_PSPAccountId",
                        column: x => x.PSPAccountId,
                        principalTable: "PSPAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PSPAccountFiles_PSPAccountId",
                table: "PSPAccountFiles",
                column: "PSPAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PSPAccountFiles");

            migrationBuilder.DropTable(
                name: "PSPAccounts");
        }
    }
}
