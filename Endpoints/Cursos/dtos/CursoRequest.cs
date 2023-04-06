namespace w_escolas.Endpoints.Cursos.dtos;

public class CursoRequest
{
    public Guid TipoDeCursoId { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public int Ordem { get; set; }
}
