using Dapper;
using Microsoft.Data.SqlClient;

namespace w_escolas.Infra.Data.DapperQueries.Matriculas;

public record MatriculasDaTemporadaRecord(
    Guid Id,
    Guid CursoId,
    Guid AlunoId,
    Guid TemporadaId,
    DateTime DataMatricula,
    bool Cancelada,
    string CodAluno,
    string NomeAluno,
    string CodCurso,
    string NomeCurso
);

public class MatriculasDaTemporadaQuery
{
    private readonly IConfiguration configuration;
    public MatriculasDaTemporadaQuery(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public IEnumerable<MatriculasDaTemporadaRecord> Get(
        string escolaId,
        string temporadaId)
    {
        var db = new SqlConnection(configuration["Database:ConnectionString"]);
        var query =
            @"
            SELECT Matriculas.Id,CursoId,AlunoId,Matriculas.TemporadaId,DataMatricula,Cancelada
	            ,Alunos.Codigo as CodAluno, Alunos.Nome as NomeAluno
	            ,Cursos.Codigo as CodCurso, Cursos.Nome as NomeCurso
            FROM Matriculas
            INNER JOIN Alunos ON Matriculas.AlunoId=Alunos.Id
            INNER JOIN Cursos ON Matriculas.CursoId=Cursos.Id
            WHERE Matriculas.EscolaId=@escolaId AND Matriculas.TemporadaId=@temporadaId
            ORDER BY Cursos.Ordem, Alunos.Nome
            ";
        return db.Query<MatriculasDaTemporadaRecord>(
            query, new { escolaId, temporadaId }
        );
    }
}
