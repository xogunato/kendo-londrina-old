namespace w_escolas.Endpoints.Alunos.dtos;

public class AlunoFilter
{
    public string? Id { get; set; }
    public string? CursoId { get; set; }
    public string? TurmaId { get; set; }
    public string? CodigoOuNome { get; set; }

    public static ValueTask<AlunoFilter?> BindAsync(HttpContext context)
    {
        var result = new AlunoFilter
        {
            Id = context.Request.Query["Id"],
            CursoId = context.Request.Query["CursoId"],
            TurmaId = context.Request.Query["TurmaId"],
            CodigoOuNome = context.Request.Query["CodigoOuNome"]
        };
        return ValueTask.FromResult<AlunoFilter?>(result);
    }
}
