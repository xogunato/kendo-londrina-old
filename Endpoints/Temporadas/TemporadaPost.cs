using w_escolas.Domain.Temporadas;
using w_escolas.Endpoints.Temporadas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Temporadas;

public class TemporadaPost
{
    public static string Template => "/temporadas";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        TemporadaRequest temporadaRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var temporada = DtoToObj(escolaIdDoUsuarioCorrente, temporadaRequest);
        var validator = new TemporadaValidator();
        var validation = validator.Validate(temporada);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeIncluir(context, temporada))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Temporadas.Add(temporada);
        context.SaveChanges();
        return Results.Created($"{Template}/{temporada.Id}", temporada.Id);
    }

    private static Temporada DtoToObj(Guid escolaId,
        TemporadaRequest temporadaRequest)
    {
        return new Temporada(
            escolaId,
            temporadaRequest.Codigo!,
            temporadaRequest.Nome!,
            temporadaRequest.Ano,
            temporadaRequest.Semestre,
            temporadaRequest.Quadrimestre,
            temporadaRequest.Trimestre,
            temporadaRequest.Bimestre,
            temporadaRequest.Mes);
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoCodigo(ApplicationDbContext context, Temporada temporada)
    {
        if (context.Temporadas.Where(t =>
            t.EscolaId == temporada.EscolaId &&
            t.Codigo == temporada.Codigo).Any())
            errorMessages.Add($"Já existe Temporada com código {temporada.Codigo}.");
    }

    private static void VerificarComMesmoNome(ApplicationDbContext context, Temporada temporada)
    {
        if (context.Temporadas.Where(t =>
            t.EscolaId == temporada.EscolaId &&
            t.Nome == temporada.Nome).Any())
                errorMessages.Add($"Já existe Temporada com nome {temporada.Nome}.");
    }

    private static bool NaoPodeIncluir(ApplicationDbContext context, Temporada temporada)
    {
        errorMessages.Clear();
        VerificarComMesmoCodigo(context, temporada);
        VerificarComMesmoNome(context, temporada);
        return errorMessages.Count > 0;
    }

}
