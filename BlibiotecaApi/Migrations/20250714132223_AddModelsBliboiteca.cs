using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlibiotecaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddModelsBliboiteca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Blbiotecas_BlibiotecaID",
                table: "Livros");

            migrationBuilder.RenameColumn(
                name: "BlibiotecaID",
                table: "Livros",
                newName: "IDBlibioteca");

            migrationBuilder.RenameIndex(
                name: "IX_Livros_BlibiotecaID",
                table: "Livros",
                newName: "IX_Livros_IDBlibioteca");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Blbiotecas_IDBlibioteca",
                table: "Livros",
                column: "IDBlibioteca",
                principalTable: "Blbiotecas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Blbiotecas_IDBlibioteca",
                table: "Livros");

            migrationBuilder.RenameColumn(
                name: "IDBlibioteca",
                table: "Livros",
                newName: "BlibiotecaID");

            migrationBuilder.RenameIndex(
                name: "IX_Livros_IDBlibioteca",
                table: "Livros",
                newName: "IX_Livros_BlibiotecaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Blbiotecas_BlibiotecaID",
                table: "Livros",
                column: "BlibiotecaID",
                principalTable: "Blbiotecas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
