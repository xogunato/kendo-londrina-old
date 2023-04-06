using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace w_escolas.Migrations
{
    public partial class AddMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EscolaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemporadaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataMatricula = table.Column<DateTime>(type: "date", nullable: false),
                    Cancelada = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    EditedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matriculas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matriculas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matriculas_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matriculas_Temporadas_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlunoId",
                table: "Matriculas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_CursoId_AlunoId_TemporadaId",
                table: "Matriculas",
                columns: new[] { "CursoId", "AlunoId", "TemporadaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_EscolaId",
                table: "Matriculas",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_TemporadaId",
                table: "Matriculas",
                column: "TemporadaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matriculas");
        }
    }
}
