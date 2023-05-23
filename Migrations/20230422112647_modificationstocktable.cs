using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class modificationstocktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Produits_ProduitId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "ProduitId",
                table: "Stocks",
                newName: "MatiereId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_ProduitId",
                table: "Stocks",
                newName: "IX_Stocks_MatiereId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Matieres_MatiereId",
                table: "Stocks",
                column: "MatiereId",
                principalTable: "Matieres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Matieres_MatiereId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "MatiereId",
                table: "Stocks",
                newName: "ProduitId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_MatiereId",
                table: "Stocks",
                newName: "IX_Stocks_ProduitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Produits_ProduitId",
                table: "Stocks",
                column: "ProduitId",
                principalTable: "Produits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
