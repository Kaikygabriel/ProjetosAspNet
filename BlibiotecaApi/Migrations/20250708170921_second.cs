using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlibiotecaApi.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlibiotecaId",
                table: "Livros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Blibiotecas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cep = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blibiotecas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_BlibiotecaId",
                table: "Livros",
                column: "BlibiotecaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Blibiotecas_BlibiotecaId",
                table: "Livros",
                column: "BlibiotecaId",
                principalTable: "Blibiotecas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Blibiotecas_BlibiotecaId",
                table: "Livros");

            migrationBuilder.DropTable(
                name: "Blibiotecas");

            migrationBuilder.DropIndex(
                name: "IX_Livros_BlibiotecaId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "BlibiotecaId",
                table: "Livros");
        }
    }
}
