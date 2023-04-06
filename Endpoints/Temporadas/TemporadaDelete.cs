using Microsoft.AspNetCore.Mvc;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Temporadas;

public class TemporadaDelete
{
    public static string Template => "/temporadas/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        [FromRoute] Guid id,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var temporada = context.Temporadas.Where(e => e.Id == id).FirstOrDefault();
        if (temporada == null)
            return Results.NotFound();

        if (temporada.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        if (NaoPodeExcluir(context, id))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Temporadas.Remove(temporada);
        context.SaveChanges();

        return Results.Ok();
    }

    private static readonly List<string> errorMessages = new();
    //private static void TemMatriculaVinculada(ApplicationDbContext context, Guid temporadaId)
    //{
    //    if (context.Matriculas.Where(t => t.TemporadaId == temporadaId).Any())
    //        errorMessages.Add("Existe(m) Matrícula(s) vinculadas");
    //}
    private static bool NaoPodeExcluir(ApplicationDbContext context, Guid temporadaId)
    {
        errorMessages.Clear();
        // TemMatriculaVinculada(context, temporadaId);
        return errorMessages.Count > 0;
    }

}
