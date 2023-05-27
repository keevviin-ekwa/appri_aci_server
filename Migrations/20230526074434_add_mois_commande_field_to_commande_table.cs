using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class add_mois_commande_field_to_commande_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCommande",
                table: "Commandes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCommande",
                table: "Commandes");
        }
    }
}
