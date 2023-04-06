namespace w_escolas.Endpoints.Matriculas.dtos;

public class MatriculaFilter
{
    public string? Id { get; set; }
    public string? CursoId { get; set; }
    public string? AlunoId { get; set; }
    public string? TemporadaId { get; set; }
    public DateTime? De { get; set; }
    public DateTime? Ate { get; set; }
    public bool? Cancelada { get; set; }

    public static ValueTask<MatriculaFilter?> BindAsync(HttpContext context)
    {
        var result = new MatriculaFilter
        {
            Id = context.Request.Query["Id"],
            CursoId = context.Request.Query["CursoId"],
            AlunoId = context.Request.Query["AlunoId"],
            TemporadaId = context.Request.Query["TemporadaId"],
            //De = context.Request.Query["De"],
            //Ate = context.Request.Query["Ate"],
            //Cancelada = context.Request.Query["Cancelada"]
        };
        return ValueTask.FromResult<MatriculaFilter?>(result);
    }
}
