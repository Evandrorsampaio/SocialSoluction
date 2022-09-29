using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialSoluctionMVC.Data.Migrations
{
    public partial class SoftDelete2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Exclusao",
                table: "Clientes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Exclusao",
                table: "Anuncios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exclusao",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Exclusao",
                table: "Anuncios");
        }
    }
}
