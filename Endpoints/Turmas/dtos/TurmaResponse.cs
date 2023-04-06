namespace w_escolas.Endpoints.Turmas.dtos;

public class TurmaComCursoResponse
{
    public Guid Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public int? Ordem { get; set; }
    public int? MaxAlunos { get; set; }
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
    public Guid CursoId { get; set; }
    public string? CodCurso { get; set; }
    public string? NomeCurso { get; set; }
}

public class TurmaResponse
{
    public Guid Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public int? Ordem { get; set; }
    public int? MaxAlunos { get; set; }
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
}
