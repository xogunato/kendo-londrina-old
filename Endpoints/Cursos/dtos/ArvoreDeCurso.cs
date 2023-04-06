namespace w_escolas.Endpoints.Turmas.dtos;

public class TipoDeCursoTreenode
{
    public Guid Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public int Ordem { get; set; }
    public List<CursoTreenode>? Cursos { get; set; }
}

public class CursoTreenode
{
    public Guid Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public int? Ordem { get; set; }
    public List<TurmaTreenode>? Turmas { get; set; }
}

public class TurmaTreenode
{
    public Guid Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public int? Ordem { get; set; }
}
