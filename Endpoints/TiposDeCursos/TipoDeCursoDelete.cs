using Microsoft.AspNetCore.Mvc;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.TiposDeCursos;

public class TipoDeCursoDelete
{
    public static string Template => "/tiposDeCursos/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        [FromRoute] Guid id,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var tipoDeCurso = context.TiposDeCursos.Where(e => e.Id == id).FirstOrDefault();
        if (tipoDeCurso == null)
            return Results.NotFound();

        if (tipoDeCurso.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        if (NaoPodeExcluir(context, id))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.TiposDeCursos.Remove(tipoDeCurso);
        context.SaveChanges();

        return Results.Ok();
    }

    private static readonly List<string> errorMessages = new();
    private static void TemCursoVinculado(ApplicationDbContext context, Guid tipoDeCursoId)
    {
        if (context.Cursos.Where(t => t.TipoDeCursoId == tipoDeCursoId).Any())
            errorMessages.Add("Existe(m) Curso(s) vinculados");
    }
    private static bool NaoPodeExcluir(ApplicationDbContext context, Guid tipoDeCursoId)
    {
        errorMessages.Clear();
        TemCursoVinculado(context, tipoDeCursoId);
        return errorMessages.Count > 0;
    }
}
