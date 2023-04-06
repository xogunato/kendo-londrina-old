namespace w_escolas.Endpoints.Cursos.dtos;

public class CursoFilter
{
    public string? Id { get; set; }
    public string? TipoDeCursoId { get; set; }
    public string? CodigoOuNome { get; set; }

    public static ValueTask<CursoFilter?> BindAsync(HttpContext context)
    {
        var result = new CursoFilter
        {
            Id = context.Request.Query["Id"],
            TipoDeCursoId = context.Request.Query["TipoDeCursoId"],
            CodigoOuNome = context.Request.Query["CodigoOuNome"]
        };

        Console.WriteLine(result);

        return ValueTask.FromResult<CursoFilter?>(result);
    }

    //public static bool TryParse(string? queryString, out CursoFilter? cursoFilter)
    //{
    //    //// Format is "(12.3,10.1)"
    //    //var trimmedValue = value?.TrimStart('(').TrimEnd(')');
    //    //var segments = trimmedValue?.Split(',',
    //    //        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    //    //if (segments?.Length == 2
    //    //    && double.TryParse(segments[0], out var x)
    //    //    && double.TryParse(segments[1], out var y))
    //    //{
    //    //    point = new Point { X = x, Y = y };
    //    //    return true;
    //    //}

    //    cursoFilter = new CursoFilter {
    //        Id = queryString,
    //        TipoDeCursoId = "",
    //        CodigoOuNome = ""
    //    };
    //    Console.WriteLine(cursoFilter);
    //    return false;

    //    //cursoFilter = null;
    //    //return false;
    //}
}
