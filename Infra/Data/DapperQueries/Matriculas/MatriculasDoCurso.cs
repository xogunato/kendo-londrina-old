using Dapper;
using Microsoft.Data.SqlClient;

namespace w_escolas.Infra.Data.DapperQueries.Matriculas;

public record MatriculasDoCursoRecord(
    Guid Id,
    Guid CursoId,
    Guid AlunoId,
    Guid TemporadaId,
    DateTime DataMatricula,
    bool Cancelada,
    string CodAluno,
    string NomeAluno,
    string CodTemporada,
    string NomeTemporada
);

public class MatriculasDoCursoQuery
{
    private readonly IConfiguration configuration;
    public MatriculasDoCursoQuery(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public IEnumerable<MatriculasDoCursoRecord> Get(
        string escolaId,
        string cursoId)
    {
        var db = new SqlConnection(configuration["Database:ConnectionString"]);
        var query =
            @"
            SELECT Matriculas.Id,CursoId,AlunoId,Matriculas.TemporadaId,DataMatricula,Cancelada
	            ,Alunos.Codigo as CodAluno, Alunos.Nome as NomeAluno
	            ,Temporadas.Codigo as CodTemporada, Temporadas.Nome as NomeTemporada
            FROM Matriculas
            INNER JOIN Alunos ON Matriculas.AlunoId=Alunos.Id
            INNER JOIN Temporadas ON Matriculas.TemporadaId=Temporadas.Id
            WHERE Matriculas.EscolaId=@escolaId AND Matriculas.CursoId=@cursoId
            ORDER BY Alunos.Nome
            ";
        return db.Query<MatriculasDoCursoRecord>(
            query, new { escolaId, cursoId }
        );
    }
}
