using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApproACI.Migrations
{
    public partial class add__datelivraison_field_to_livraison_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateLivraison",
                table: "Livraisons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLivraison",
                table: "Livraisons");
        }
    }
}
