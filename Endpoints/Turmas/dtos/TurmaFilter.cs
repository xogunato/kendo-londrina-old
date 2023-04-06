namespace w_escolas.Endpoints.Turmas.dtos;

public class TurmaFilter
{
    public string? Id { get; set; }
    public string? CursoId { get; set; }
    public string? CodigoOuNome { get; set; }

    public static ValueTask<TurmaFilter?> BindAsync(HttpContext context)
    {
        var result = new TurmaFilter
        {
            Id = context.Request.Query["Id"],
            CursoId = context.Request.Query["CursoId"],
            CodigoOuNome = context.Request.Query["CodigoOuNome"]
        };
        return ValueTask.FromResult<TurmaFilter?>(result);
    }
}
