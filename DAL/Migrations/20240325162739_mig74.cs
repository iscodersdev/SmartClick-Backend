using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class mig74 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubProductoId",
                table: "ComprasProductos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComprasProductos_SubProductoId",
                table: "ComprasProductos",
                column: "SubProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasProductos_SubProductos_SubProductoId",
                table: "ComprasProductos",
                column: "SubProductoId",
                principalTable: "SubProductos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComprasProductos_SubProductos_SubProductoId",
                table: "ComprasProductos");

            migrationBuilder.DropIndex(
                name: "IX_ComprasProductos_SubProductoId",
                table: "ComprasProductos");

            migrationBuilder.DropColumn(
                name: "SubProductoId",
                table: "ComprasProductos");
        }
    }
}
