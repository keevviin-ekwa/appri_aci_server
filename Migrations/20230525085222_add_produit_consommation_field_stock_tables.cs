using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class add_produit_consommation_field_stock_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Consommation",
                table: "Stocks",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProduitId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProduitId",
                table: "Stocks",
                column: "ProduitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Produits_ProduitId",
                table: "Stocks",
                column: "ProduitId",
                principalTable: "Produits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Produits_ProduitId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProduitId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ProduitId",
                table: "Stocks");

            migrationBuilder.AlterColumn<int>(
                name: "Consommation",
                table: "Stocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
