using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlibiotecaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlibiotecaID",
                table: "Livros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Blbiotecas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blbiotecas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_BlibiotecaID",
                table: "Livros",
                column: "BlibiotecaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Blbiotecas_BlibiotecaID",
                table: "Livros",
                column: "BlibiotecaID",
                principalTable: "Blbiotecas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Blbiotecas_BlibiotecaID",
                table: "Livros");

            migrationBuilder.DropTable(
                name: "Blbiotecas");

            migrationBuilder.DropIndex(
                name: "IX_Livros_BlibiotecaID",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "BlibiotecaID",
                table: "Livros");
        }
    }
}
