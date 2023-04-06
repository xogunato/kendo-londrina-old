using Microsoft.AspNetCore.Mvc;
using w_escolas.Domain.Cursos;
using w_escolas.Domain.Turmas;
using w_escolas.Endpoints.Cursos.dtos;
using w_escolas.Endpoints.Turmas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Turmas;

public class TurmaPut
{
    public static string Template => "/turmas/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action([FromRoute] Guid id,
        TurmaRequest turmaRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var turma = context.Turmas.Where(e => e.Id == id).FirstOrDefault();
        if (turma == null)
            return Results.NotFound();

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();
        if (turma.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        turma.Alterar(turmaRequest.CursoId,
            turmaRequest.Codigo!,
            turmaRequest.Nome!,
            turmaRequest.Ordem,
            turmaRequest.MaxAlunos,
            turmaRequest.DataInicial,
            turmaRequest.DataFinal);
        var validator = new TurmaValidator();
        var validation = validator.Validate(turma);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeAlterar(context, turma))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Turmas.Update(turma);
        context.SaveChanges();
        return Results.Ok();
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoCodigo(ApplicationDbContext context, Turma turma)
    {
        if (context.Turmas.Where(t =>
            t.EscolaId == turma.EscolaId &&
            t.Codigo == turma.Codigo &&
            t.Id != turma.Id).Any())
            errorMessages.Add($"Já existe Turma com código {turma.Codigo}.");
    }

    private static void VerificarComMesmoNome(ApplicationDbContext context, Turma turma)
    {
        if (context.Turmas.Where(t =>
            t.EscolaId == turma.EscolaId &&
            t.Nome == turma.Nome &&
            t.Id != turma.Id).Any())
            errorMessages.Add($"Já existe Turma com nome {turma.Nome}.");
    }

    private static bool NaoPodeAlterar(ApplicationDbContext context, Turma turma)
    {
        errorMessages.Clear();
        VerificarComMesmoCodigo(context, turma);
        VerificarComMesmoNome(context, turma);
        return errorMessages.Count > 0;
    }

}
