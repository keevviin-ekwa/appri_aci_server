using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class create_goal_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContributionMatiere",
                table: "MatiereProduits",
                newName: "contributionMatierePF");

            migrationBuilder.AddColumn<double>(
                name: "ContributionMatiereGF",
                table: "MatiereProduits",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContributionMatiereGF",
                table: "MatiereProduits");

            migrationBuilder.RenameColumn(
                name: "contributionMatierePF",
                table: "MatiereProduits",
                newName: "ContributionMatiere");
        }
    }
}
