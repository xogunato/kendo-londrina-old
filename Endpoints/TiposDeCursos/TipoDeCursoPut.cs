using Microsoft.AspNetCore.Mvc;
using w_escolas.Domain.TiposDeCursos;
using w_escolas.Endpoints.TiposDeCursos.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.TiposDeCursos;

public class TipoDeCursoPut
{
    public static string Template => "/tiposdecursos/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id,
        TipoDeCursoRequest tipoDeCursoRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var tipoDeCurso = context.TiposDeCursos.Where(e => e.Id == id).FirstOrDefault();
        if (tipoDeCurso == null)
            return Results.NotFound();

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();
        if (tipoDeCurso.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        tipoDeCurso.Alterar(tipoDeCursoRequest.Codigo!,
            tipoDeCursoRequest.Nome!,
            tipoDeCursoRequest.Ordem);
        var validator = new TipoDeCursoValidator();
        var validation = validator.Validate(tipoDeCurso);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeAlterar(context, tipoDeCurso))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.TiposDeCursos.Update(tipoDeCurso);
        context.SaveChanges();
        return Results.Ok();
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoCodigo(ApplicationDbContext context, TipoDeCurso tipoDeCurso)
    {
        if (context.TiposDeCursos.Where(
            t => t.Codigo == tipoDeCurso.Codigo
            && t.Id != tipoDeCurso.Id).Any())
            errorMessages.Add($"Já existe Tipo de Curso com código {tipoDeCurso.Codigo}.");
    }

    private static void VerificarComMesmoNome(ApplicationDbContext context, TipoDeCurso tipoDeCurso)
    {
        if (context.TiposDeCursos.Where(
            t => t.Nome == tipoDeCurso.Nome
            && t.Id != tipoDeCurso.Id).Any())
            errorMessages.Add($"Já existe Tipo de Curso com nome {tipoDeCurso.Nome}.");
    }

    private static bool NaoPodeAlterar(ApplicationDbContext context, TipoDeCurso tipoDeCurso)
    {
        errorMessages.Clear();
        VerificarComMesmoCodigo(context, tipoDeCurso);
        VerificarComMesmoNome(context, tipoDeCurso);
        return errorMessages.Count > 0;
    }
}
