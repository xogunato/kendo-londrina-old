using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace w_escolas.Migrations
{
    public partial class Enderecos_camelcase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UF",
                table: "Enderecos",
                newName: "Uf");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "Enderecos",
                newName: "Cep");

            migrationBuilder.RenameColumn(
                name: "UFNascimento",
                table: "Alunos",
                newName: "UfNascimento");

            migrationBuilder.RenameColumn(
                name: "RG",
                table: "Alunos",
                newName: "Rg");

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Alunos",
                newName: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_EnderecoId",
                table: "Alunos",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Enderecos_EnderecoId",
                table: "Alunos",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Enderecos_EnderecoId",
                table: "Alunos");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_EnderecoId",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "Uf",
                table: "Enderecos",
                newName: "UF");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "Enderecos",
                newName: "CEP");

            migrationBuilder.RenameColumn(
                name: "UfNascimento",
                table: "Alunos",
                newName: "UFNascimento");

            migrationBuilder.RenameColumn(
                name: "Rg",
                table: "Alunos",
                newName: "RG");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Alunos",
                newName: "CPF");
        }
    }
}
