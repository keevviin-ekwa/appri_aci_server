using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class create_goal_field_correct_objectif_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ObjectifAttendu",
                table: "Objectif",
                newName: "ObjectifPF");

            migrationBuilder.RenameColumn(
                name: "ObjectifAtteind",
                table: "Objectif",
                newName: "ObjectifGF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ObjectifPF",
                table: "Objectif",
                newName: "ObjectifAttendu");

            migrationBuilder.RenameColumn(
                name: "ObjectifGF",
                table: "Objectif",
                newName: "ObjectifAtteind");
        }
    }
}
