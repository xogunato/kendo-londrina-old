using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace w_escolas.Migrations
{
    public partial class AddEnderecos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CEP = table.Column<string>(type: "char(9)", maxLength: 9, nullable: false),
                    UF = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Distrito = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Tipo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    EditedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
