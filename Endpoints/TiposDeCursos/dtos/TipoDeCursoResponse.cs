namespace w_escolas.Endpoints.TiposDeCursos.dtos;

public class TipoDeCursoResponse
{
    public Guid Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public int? Ordem { get; set; }
}
