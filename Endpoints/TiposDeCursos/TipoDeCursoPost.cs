using w_escolas.Domain.Cursos;
using w_escolas.Domain.TiposDeCursos;
using w_escolas.Endpoints.TiposDeCursos.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.TiposDeCursos;

public class TipoDeCursoPost
{
    public static string Template => "/tiposdecursos";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(
        TipoDeCursoRequest tipoDeCursoRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var tipoDeCurso = DtoToObj(escolaIdDoUsuarioCorrente, tipoDeCursoRequest);
        var validator = new TipoDeCursoValidator();
        var validation = validator.Validate(tipoDeCurso);
        if(!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeIncluir(context, tipoDeCurso))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.TiposDeCursos.Add(tipoDeCurso);
        context.SaveChanges();
        return Results.Created($"{Template}/{tipoDeCurso.Id}", tipoDeCurso.Id);
    }

    private static TipoDeCurso DtoToObj(Guid escolaId,
        TipoDeCursoRequest tipoDeCursoRequest)
    {
        return new TipoDeCurso(
            escolaId,
            tipoDeCursoRequest.Codigo!,
            tipoDeCursoRequest.Nome!,
            tipoDeCursoRequest.Ordem);
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoCodigo(ApplicationDbContext context, TipoDeCurso tipoDeCurso)
    {
        if (context.TiposDeCursos.Where(t => 
            t.Codigo == tipoDeCurso.Codigo &&
            t.EscolaId == tipoDeCurso.EscolaId).Any())
                errorMessages.Add($"Já existe Tipo de Curso com código {tipoDeCurso.Codigo}.");
    }

    private static void VerificarComMesmoNome(ApplicationDbContext context, TipoDeCurso tipoDeCurso)
    {
        if (context.TiposDeCursos.Where(t =>
            t.Nome == tipoDeCurso.Nome &&
            t.EscolaId == tipoDeCurso.EscolaId).Any())
            errorMessages.Add($"Já existe Tipo de Curso com nome {tipoDeCurso.Nome}.");
    }

    private static bool NaoPodeIncluir(ApplicationDbContext context, TipoDeCurso tipoDeCurso)
    {
        errorMessages.Clear();
        VerificarComMesmoCodigo(context, tipoDeCurso);
        VerificarComMesmoNome(context, tipoDeCurso);
        return errorMessages.Count > 0;
    }
}
