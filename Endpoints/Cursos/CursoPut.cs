using Microsoft.AspNetCore.Mvc;
using w_escolas.Domain.Cursos;
using w_escolas.Domain.TiposDeCursos;
using w_escolas.Endpoints.Cursos.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Cursos;

public class CursoPut
{
    public static string Template => "/cursos/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action([FromRoute] Guid id,
        CursoRequest cursoRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var curso = context.Cursos.Where(e => e.Id == id).FirstOrDefault();
        if (curso == null)
            return Results.NotFound();

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();
        if (curso.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        curso.Alterar(cursoRequest.TipoDeCursoId,
            cursoRequest.Codigo!,
            cursoRequest.Nome!,
            cursoRequest.Ordem);
        var validator = new CursoValidator();
        var validation = validator.Validate(curso);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeAlterar(context, curso))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Cursos.Update(curso);
        context.SaveChanges();
        return Results.Ok();
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoCodigo(ApplicationDbContext context, Curso curso)
    {
        if (context.Cursos.Where(t =>
            t.EscolaId == curso.EscolaId &&
            t.Codigo == curso.Codigo &&
            t.Id != curso.Id).Any())
                errorMessages.Add($"Já existe Curso com código {curso.Codigo}.");
    }

    private static void VerificarComMesmoNome(ApplicationDbContext context, Curso curso)
    {
        if (context.Cursos.Where(t =>
            t.EscolaId == curso.EscolaId &&
            t.Nome == curso.Nome &&
            t.Id != curso.Id).Any())
                errorMessages.Add($"Já existe Curso com nome {curso.Nome}.");
    }

    private static bool NaoPodeAlterar(ApplicationDbContext context, Curso curso)
    {
        errorMessages.Clear();
        VerificarComMesmoCodigo(context, curso);
        VerificarComMesmoNome(context, curso);
        return errorMessages.Count > 0;
    }
}
