using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class add_commande_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjectifAtteindGF",
                table: "Objectif");

            migrationBuilder.DropColumn(
                name: "ObjectifAtteindPF",
                table: "Objectif");

            migrationBuilder.AddColumn<int>(
                name: "ProduitId",
                table: "Objectif",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProduitId",
                table: "Objectif");

            migrationBuilder.AddColumn<double>(
                name: "ObjectifAtteindGF",
                table: "Objectif",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ObjectifAtteindPF",
                table: "Objectif",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
