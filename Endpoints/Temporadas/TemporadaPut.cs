using Microsoft.AspNetCore.Mvc;
using w_escolas.Domain.Temporadas;
using w_escolas.Endpoints.Temporadas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Temporadas;

public class TemporadaPut
{
    public static string Template => "/temporadas/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id,
        TemporadaRequest temporadaRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var temporada = context.Temporadas.Where(e => e.Id == id).FirstOrDefault();
        if (temporada == null)
            return Results.NotFound();

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();
        if (temporada.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        temporada.Alterar(temporadaRequest.Codigo!,
            temporadaRequest.Nome!,
            temporadaRequest.Ano,
            temporadaRequest.Semestre,
            temporadaRequest.Quadrimestre,
            temporadaRequest.Trimestre,
            temporadaRequest.Bimestre,
            temporadaRequest.Mes);
        var validator = new TemporadaValidator();
        var validation = validator.Validate(temporada);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeAlterar(context, temporada))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Temporadas.Update(temporada);
        context.SaveChanges();
        return Results.Ok();
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoCodigo(ApplicationDbContext context, Temporada temporada)
    {
        if (context.Temporadas.Where(
            t => t.Codigo == temporada.Codigo
            && t.Id != temporada.Id).Any())
            errorMessages.Add($"Já existe Temporada com código {temporada.Codigo}.");
    }

    private static void VerificarComMesmoNome(ApplicationDbContext context, Temporada temporada)
    {
        if (context.Temporadas.Where(
            t => t.Nome == temporada.Nome
            && t.Id != temporada.Id).Any())
            errorMessages.Add($"Já existe Temporada com nome {temporada.Nome}.");
    }

    private static bool NaoPodeAlterar(ApplicationDbContext context, Temporada temporada)
    {
        errorMessages.Clear();
        VerificarComMesmoCodigo(context, temporada);
        VerificarComMesmoNome(context, temporada);
        return errorMessages.Count > 0;
    }

}
