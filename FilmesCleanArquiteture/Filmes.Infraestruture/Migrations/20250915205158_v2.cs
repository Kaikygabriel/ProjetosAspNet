using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmes.Infraestruture.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredRefreshToken",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredRefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");
        }
    }
}
