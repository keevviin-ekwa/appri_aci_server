using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class add_commandes_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Objectif_ProduitId",
                table: "Objectif",
                column: "ProduitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectif_Produits_ProduitId",
                table: "Objectif",
                column: "ProduitId",
                principalTable: "Produits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectif_Produits_ProduitId",
                table: "Objectif");

            migrationBuilder.DropIndex(
                name: "IX_Objectif_ProduitId",
                table: "Objectif");
        }
    }
}
