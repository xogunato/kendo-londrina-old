using Dapper;
using Microsoft.Data.SqlClient;

namespace w_escolas.Infra.Data.DapperQueries.Matriculas;

public record MatriculasDoAlunoRecord(
    Guid Id,
    Guid CursoId,
    Guid AlunoId,
    Guid TemporadaId,
    DateTime DataMatricula,
    bool Cancelada,
    string CodCurso,
    string NomeCurso,
    string CodTemporada,
    string NomeTemporada
);

public class MatriculasDoAlunoQuery
{
    private readonly IConfiguration configuration;
    public MatriculasDoAlunoQuery(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public IEnumerable<MatriculasDoAlunoRecord> Get(
        string escolaId,
        string alunoId)
    {
        var db = new SqlConnection(configuration["Database:ConnectionString"]);
        var query =
            @"
            SELECT Matriculas.Id,CursoId,AlunoId,Matriculas.TemporadaId,DataMatricula,Cancelada
	            ,Cursos.Codigo as CodCurso, Cursos.Nome as NomeCurso
	            ,Temporadas.Codigo as CodTemporada, Temporadas.Nome as NomeTemporada
            FROM Matriculas
            INNER JOIN Cursos ON Matriculas.CursoId=Cursos.Id
            INNER JOIN Temporadas ON Matriculas.TemporadaId=Temporadas.Id
            WHERE Matriculas.EscolaId=@escolaId AND Matriculas.AlunoId=@alunoId
            ORDER BY Temporadas.Ano,Temporadas.Semestre,Temporadas.Quadrimestre,Temporadas.Trimestre,Temporadas.Bimestre,Temporadas.Mes
                ,Cursos.Ordem
            ";
        return db.Query<MatriculasDoAlunoRecord>(
            query, new { escolaId, alunoId }
        );
    }
}
