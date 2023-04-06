namespace w_escolas.Endpoints.Temporadas.dtos;

public class TemporadaFilter
{
    public string? Id { get; set; }
    public string? CodigoOuNome { get; set; }
    public string? Ano { get; set; }
    public Boolean? SemAno { get; set; }

    public static ValueTask<TemporadaFilter?> BindAsync(HttpContext context)
    {
        var result = new TemporadaFilter
        {
            Id = context.Request.Query["Id"],
            CodigoOuNome = context.Request.Query["CodigoOuNome"],
            Ano = context.Request.Query["Ano"],
            SemAno = Convert.ToBoolean(context.Request.Query["SemAno"])
        };
        return ValueTask.FromResult<TemporadaFilter?>(result);
    }
}
