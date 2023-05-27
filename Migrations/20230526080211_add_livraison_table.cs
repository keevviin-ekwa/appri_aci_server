using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class add_livraison_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livraisons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandeId = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livraisons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livraisons_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livraisons_CommandeId",
                table: "Livraisons",
                column: "CommandeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livraisons");
        }
    }
}
