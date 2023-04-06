using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace w_escolas.Migrations
{
    public partial class Alunos_Codigo_not_required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Alunos_EscolaId_Codigo",
                table: "Alunos");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Alunos",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_EscolaId",
                table: "Alunos",
                column: "EscolaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Alunos_EscolaId",
                table: "Alunos");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Alunos",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_EscolaId_Codigo",
                table: "Alunos",
                columns: new[] { "EscolaId", "Codigo" },
                unique: true);
        }
    }
}
