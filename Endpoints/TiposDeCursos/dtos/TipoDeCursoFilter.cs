using w_escolas.Endpoints.Cursos.dtos;

namespace w_escolas.Endpoints.TiposDeCursos.dtos;

public class TipoDeCursoFilter
{
    public string? Id { get; set; }
    public string? CodigoOuNome { get; set; }

    public static ValueTask<TipoDeCursoFilter?> BindAsync(HttpContext context)
    {
        var result = new TipoDeCursoFilter
        {
            Id = context.Request.Query["Id"],
            CodigoOuNome = context.Request.Query["CodigoOuNome"]
        };

        Console.WriteLine(result);

        return ValueTask.FromResult<TipoDeCursoFilter?>(result);
    }
}
