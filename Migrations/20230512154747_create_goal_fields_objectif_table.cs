using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class create_goal_fields_objectif_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjectifAtteindGF",
                table: "Objectif");

            migrationBuilder.DropColumn(
                name: "ObjectifAtteindPF",
                table: "Objectif");
        }
    }
}
