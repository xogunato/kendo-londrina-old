using w_escolas.Domain.Turmas;
using w_escolas.Endpoints.Turmas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Turmas;

public class TurmaPost
{
    public static string Template => "/turmas";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(
        TurmaRequest turmaRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var turma = DtoToObj(escolaIdDoUsuarioCorrente, turmaRequest);
        var validator = new TurmaValidator();
        var validation = validator.Validate(turma);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeIncluir(context, turma))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Turmas.Add(turma);
        context.SaveChanges();
        return Results.Created($"{Template}/{turma.Id}", turma.Id);
    }

    private static Turma DtoToObj(Guid escolaId, TurmaRequest turmaRequest)
    {
        return new Turma(
            escolaId,
            turmaRequest.CursoId,
            turmaRequest.Codigo!,
            turmaRequest.Nome!,
            turmaRequest.Ordem,
            turmaRequest.MaxAlunos,
            turmaRequest.DataInicial,
            turmaRequest.DataFinal);
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoCodigo(ApplicationDbContext context, Turma turma)
    {
        if (context.Turmas.Where(t =>
            t.Codigo == turma.Codigo &&
            t.EscolaId == turma.EscolaId).Any())
            errorMessages.Add($"Já existe Turma com código {turma.Codigo}.");
    }

    private static void VerificarComMesmoNome(ApplicationDbContext context, Turma turma)
    {
        if (context.Turmas.Where(t =>
            t.Nome == turma.Nome &&
            t.EscolaId == turma.EscolaId).Any())
            errorMessages.Add($"Já existe Turma com nome {turma.Nome}.");
    }

    private static bool NaoPodeIncluir(ApplicationDbContext context, Turma turma)
    {
        errorMessages.Clear();
        VerificarComMesmoCodigo(context, turma);
        VerificarComMesmoNome(context, turma);
        return errorMessages.Count > 0;
    }

}
