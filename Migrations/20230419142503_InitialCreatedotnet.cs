using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class InitialCreatedotnet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matieres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Origine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrixUnitaire = table.Column<double>(type: "float", nullable: false),
                    Colissage = table.Column<double>(type: "float", nullable: false),
                    DelaisAppro = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matieres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatiereProduit",
                columns: table => new
                {
                    MatieresId = table.Column<int>(type: "int", nullable: false),
                    ProduitsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatiereProduit", x => new { x.MatieresId, x.ProduitsId });
                    table.ForeignKey(
                        name: "FK_MatiereProduit_Matieres_MatieresId",
                        column: x => x.MatieresId,
                        principalTable: "Matieres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatiereProduit_Produits_ProduitsId",
                        column: x => x.ProduitsId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatiereProduits",
                columns: table => new
                {
                    ProduitId = table.Column<int>(type: "int", nullable: false),
                    MatiereId = table.Column<int>(type: "int", nullable: false),
                    ContributionMatiere = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatiereProduits", x => new { x.MatiereId, x.ProduitId });
                    table.ForeignKey(
                        name: "FK_MatiereProduits_Matieres_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matieres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatiereProduits_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatiereProduit_ProduitsId",
                table: "MatiereProduit",
                column: "ProduitsId");

            migrationBuilder.CreateIndex(
                name: "IX_MatiereProduits_ProduitId",
                table: "MatiereProduits",
                column: "ProduitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatiereProduit");

            migrationBuilder.DropTable(
                name: "MatiereProduits");

            migrationBuilder.DropTable(
                name: "Matieres");

            migrationBuilder.DropTable(
                name: "Produits");
        }
    }
}
