using w_escolas.Domain.Matriculas;
using w_escolas.Endpoints.Matriculas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Matriculas;

public class MatriculaPost
{
    public static string Template => "/matriculas";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        MatriculaNovaRequest matriculaRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var matricula = DtoToObj(escolaIdDoUsuarioCorrente, matriculaRequest);
        var validator = new MatriculaValidator();
        var validation = validator.Validate(matricula);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeIncluir(context, matricula))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Matriculas.Add(matricula);
        context.SaveChanges();
        return Results.Created($"{Template}/{matricula.Id}", matricula.Id);
    }

    private static Matricula DtoToObj(Guid escolaId,
        MatriculaNovaRequest matriculaRequest)
    {
        return new Matricula(
            escolaId,
            matriculaRequest.CursoId,
            matriculaRequest.AlunoId,
            matriculaRequest.TemporadaId,
            matriculaRequest.DataMatricula);
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarDuplicidade(ApplicationDbContext context, Matricula matricula)
    {
        if (context.Matriculas.Where(t =>
            t.CursoId == matricula.CursoId &&
            t.AlunoId == matricula.AlunoId &&
            t.TemporadaId == matricula.TemporadaId).Any())
                errorMessages.Add($"Já existe Matrícula para este Aluno/Curso/Temporada.");
    }

    private static bool NaoPodeIncluir(ApplicationDbContext context, Matricula matricula)
    {
        errorMessages.Clear();
        VerificarDuplicidade(context, matricula);
        return errorMessages.Count > 0;
    }

}
