using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace w_escolas.Migrations
{
    public partial class AddAlunos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EscolaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    EditedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Nacionalidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    UFNascimento = table.Column<string>(type: "char(2)", maxLength: 2, nullable: true),
                    CidadeNascimento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Sexo = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    RG = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    CPF = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    TelCelular = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Religiao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_EscolaId_Codigo",
                table: "Alunos",
                columns: new[] { "EscolaId", "Codigo" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");
        }
    }
}
