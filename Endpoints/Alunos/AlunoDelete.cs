using Microsoft.AspNetCore.Mvc;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Alunos;

public class AlunoDelete
{
    public static string Template => "/alunos/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        [FromRoute] Guid id,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var aluno = context.Alunos.Where(e => e.Id == id).FirstOrDefault();
        if (aluno == null)
            return Results.NotFound();

        if (aluno.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        if (NaoPodeExcluir(context, id))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Alunos.Remove(aluno);
        context.SaveChanges();

        return Results.Ok();
    }

    private static readonly List<string> errorMessages = new();
    private static void TemNotaLancada(ApplicationDbContext context, Guid alunoId)
    {
        //if (context.Turmas.Where(t => t.CursoId == alunoId).Any())
        //    errorMessages.Add("Existem Turmas(s) vinculada(s)");
    }
    private static void TemMatricula(ApplicationDbContext context, Guid alunoId)
    {
        if (context.Matriculas.Where(t => t.AlunoId == alunoId).Any())
            errorMessages.Add("Existe(m) Matrícula(s) vinculada(s)");
    }

    private static bool NaoPodeExcluir(ApplicationDbContext context, Guid alunoId)
    {
        errorMessages.Clear();
        TemNotaLancada(context, alunoId);
        TemMatricula(context, alunoId);
        return errorMessages.Count > 0;
    }
}
