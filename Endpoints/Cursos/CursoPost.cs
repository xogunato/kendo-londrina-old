using w_escolas.Domain.Cursos;
using w_escolas.Endpoints.Cursos.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Cursos;

public class CursoPost
{
    public static string Template => "/cursos";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(
        CursoRequest cursoRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var curso = DtoToObj(escolaIdDoUsuarioCorrente, cursoRequest);
        var validator = new CursoValidator();
        var validation = validator.Validate(curso);
        if(!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeIncluir(context, curso))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Cursos.Add(curso);
        context.SaveChanges();
        return Results.Created($"{Template}/{curso.Id}", curso.Id);
    }

    private static Curso DtoToObj(Guid escolaId, CursoRequest cursoRequest)
    {
        return new Curso(
            cursoRequest.TipoDeCursoId,
            cursoRequest.Codigo!,
            cursoRequest.Nome!,
            cursoRequest.Ordem,
            escolaId);
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoCodigo(ApplicationDbContext context, Curso curso)
    {
        if (context.Cursos.Where(t =>
            t.Codigo == curso.Codigo &&
            t.EscolaId == curso.EscolaId).Any())
                errorMessages.Add($"Já existe Curso com código {curso.Codigo}.");
    }

    private static void VerificarComMesmoNome(ApplicationDbContext context, Curso curso)
    {
        if (context.Cursos.Where(t =>
            t.Nome == curso.Nome &&
            t.EscolaId == curso.EscolaId).Any())
                errorMessages.Add($"Já existe Curso com nome {curso.Nome}.");
    }

    private static bool NaoPodeIncluir(ApplicationDbContext context, Curso curso)
    {
        errorMessages.Clear();
        VerificarComMesmoCodigo(context, curso);
        VerificarComMesmoNome(context, curso);
        return errorMessages.Count > 0;
    }
}
