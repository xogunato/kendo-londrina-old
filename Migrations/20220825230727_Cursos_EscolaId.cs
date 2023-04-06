using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace w_escolas.Migrations
{
    public partial class Cursos_EscolaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EscolaId",
                table: "Cursos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_EscolaId",
                table: "Cursos",
                column: "EscolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Escolas_EscolaId",
                table: "Cursos",
                column: "EscolaId",
                principalTable: "Escolas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Escolas_EscolaId",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_EscolaId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "Cursos");
        }
    }
}
