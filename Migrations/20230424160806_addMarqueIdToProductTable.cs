using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class addMarqueIdToProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarqueId",
                table: "Produits",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Produits_MarqueId",
                table: "Produits",
                column: "MarqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Marques_MarqueId",
                table: "Produits",
                column: "MarqueId",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Marques_MarqueId",
                table: "Produits");

            migrationBuilder.DropIndex(
                name: "IX_Produits_MarqueId",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "MarqueId",
                table: "Produits");
        }
    }
}
