namespace w_escolas.Endpoints.Matriculas.dtos;

public class MatriculaNovaRequest
{
    public Guid CursoId { get; set; }
    public Guid AlunoId { get; set; }
    public Guid TemporadaId { get; set; }
    public DateTime DataMatricula { get; set; }
}
