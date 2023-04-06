using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace w_escolas.Migrations
{
    public partial class AddTemporadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TemporadaId",
                table: "Cursos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Temporadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EscolaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Ano = table.Column<short>(type: "smallint", nullable: true),
                    Semestre = table.Column<byte>(type: "tinyint", nullable: true),
                    Quadrimestre = table.Column<byte>(type: "tinyint", nullable: true),
                    Trimestre = table.Column<byte>(type: "tinyint", nullable: true),
                    Bimestre = table.Column<byte>(type: "tinyint", nullable: true),
                    Mes = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    EditedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temporadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temporadas_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_TemporadaId",
                table: "Cursos",
                column: "TemporadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Temporadas_EscolaId_Codigo",
                table: "Temporadas",
                columns: new[] { "EscolaId", "Codigo" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Temporadas_TemporadaId",
                table: "Cursos",
                column: "TemporadaId",
                principalTable: "Temporadas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Temporadas_TemporadaId",
                table: "Cursos");

            migrationBuilder.DropTable(
                name: "Temporadas");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_TemporadaId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "TemporadaId",
                table: "Cursos");
        }
    }
}
