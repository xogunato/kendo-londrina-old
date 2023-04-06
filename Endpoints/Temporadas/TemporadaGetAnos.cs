using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Temporadas;

public class TemporadaGetAnos
{
    public static string Template => "/temporadas/anos";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var anos = GetDistinctAno(context, escolaIdDoUsuarioCorrente);

        return Results.Ok(anos);
    }

    private static IQueryable<int?> GetDistinctAno(
        ApplicationDbContext context,
        Guid escolaId)
    {
        var anos = context.Temporadas
            .Where(t => t.EscolaId == escolaId)
            .Select(t => t.Ano)
            .Distinct();
        return anos;
    }
}
