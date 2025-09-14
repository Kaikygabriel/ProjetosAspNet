using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotifiMe.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredRefreshToken",
                table: "Providers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Providers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredRefreshToken",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Providers");
        }
    }
}
