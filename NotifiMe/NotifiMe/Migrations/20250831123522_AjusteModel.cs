using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotifiMe.Migrations
{
    /// <inheritdoc />
    public partial class AjusteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Providers_ProviderId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Appointments",
                newName: "providerId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ProviderId",
                table: "Appointments",
                newName: "IX_Appointments_providerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Providers_providerId",
                table: "Appointments",
                column: "providerId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Providers_providerId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "providerId",
                table: "Appointments",
                newName: "ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_providerId",
                table: "Appointments",
                newName: "IX_Appointments_ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Providers_ProviderId",
                table: "Appointments",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
