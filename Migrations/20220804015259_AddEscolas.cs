using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace w_escolas.Migrations
{
    public partial class AddEscolas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escolas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeFantasia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Cnpj = table.Column<string>(type: "char(14)", maxLength: 14, nullable: true),
                    RazaoSocial = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Cep = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Uf = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Endereco = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Telefone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Website = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    EditedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Escolas_NomeFantasia",
                table: "Escolas",
                column: "NomeFantasia",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Escolas");
        }
    }
}
