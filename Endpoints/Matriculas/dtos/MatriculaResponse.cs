using w_escolas.Domain.Alunos;
using w_escolas.Domain.Cursos;
using w_escolas.Domain.Temporadas;

namespace w_escolas.Endpoints.Matriculas.dtos;

public class MatriculaResponse
{
    public Guid Id { get; set; }
    public Guid CursoId { get; set; }
    public Guid AlunoId { get; set; }
    public Guid TemporadaId { get; set; }
    public DateTime DataMatricula { get; set; }
    public bool Cancelada { get; set; }

    public Curso? Curso { get; set; }
    public Aluno? Aluno { get; set; }
    public Temporada? Temporada { get; set; }
}
