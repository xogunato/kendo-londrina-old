namespace w_escolas.Endpoints.Cursos.dtos;

public class CursoComTipoResponse
{
    public Guid Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public int? Ordem { get; set; }
    public Guid TipoDeCursoId { get; set; }
    public string? CodTipoDeCurso { get; set; }
    public string? NomeTipoDeCurso { get; set; }
}

public class CursoResponse
{
    public Guid Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public int? Ordem { get; set; }
}


//public class ArvoreDeCursoResponse
//{
//    public Guid TipoDeCursoId { get; set; }
//    public string? CodTipoDeCurso { get; set; }
//    public string? NomeTipoDeCurso { get; set; }
//    public int Ordem { get; set; }
//    public List<CursoResponse>? Cursos { get; set; }
//}
