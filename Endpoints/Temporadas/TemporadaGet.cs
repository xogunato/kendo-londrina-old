using w_escolas.Domain.Temporadas;
using w_escolas.Endpoints.Temporadas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Temporadas;

public class TemporadaGet
{
    public static string Template => "/temporadas";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        TemporadaFilter? filter,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var temporadas = Get(context, filter!, escolaIdDoUsuarioCorrente);

        var response = temporadas.Select(
            t => new TemporadaResponse
            {
                Id = t.Id,
                Codigo = t.Codigo,
                Nome = t.Nome,
                Ano = t.Ano,
                Semestre = t.Semestre,
                Quadrimestre = t.Quadrimestre,
                Trimestre = t.Trimestre,
                Bimestre = t.Bimestre,
                Mes = t.Mes
            }
        );

        return Results.Ok(response);
    }

    private static List<Temporada> Get(
        ApplicationDbContext context,
        TemporadaFilter filter,
        Guid escolaId)
    {
        if (filter.Id != null && filter.Id != "")
        {
            Console.WriteLine(">>>>>>>>>> Temporada ESPECÍFICA POR ID");
            return GetById(context, filter.Id);
        }

        if (filter.SemAno.HasValue && filter.SemAno.Value)
        {
            Console.WriteLine(">>>>>>>>>> Temporadas SEM ANO DEFINIDO");
            return TemporadasSemAno(context, filter, escolaId);
        }

        if (filter.Ano == null)
        {
            Console.WriteLine(">>>>>>>>>> Temporadas SEM FILTRO DE ANO");
            return TemporadasSemFiltroDeAno(context, filter, escolaId);
        }

        if (filter.Ano != "")
        {
            Console.WriteLine(">>>>>>>>>> Temporadas FILTRADAS PELO ANO");
            Console.WriteLine(filter.Ano);
            return TemporadasDoAno(context, filter, escolaId);
        }

        Console.WriteLine(">>>>>>>>>> Temporadas SEM FILTRO");
        return GetAll(context, escolaId);
    }

    private static List<Temporada> GetAll(ApplicationDbContext context, Guid escolaId)
    {
        return context.Temporadas
            .Where(t => t.EscolaId == escolaId)
            .OrderBy(t => t.Nome).ToList();
    }
    private static List<Temporada> GetById(ApplicationDbContext context, string id)
    {
        return context.Temporadas
            .Where(t => t.Id.ToString() == id).ToList();
    }

    private static List<Temporada> TemporadasDoAno(
        ApplicationDbContext context,
        TemporadaFilter filter,
        Guid escolaId)
    {
        return context.Temporadas
            .Where(t => t.EscolaId == escolaId && t.Ano.ToString() == filter.Ano
                && (filter.CodigoOuNome == null
                    || t.Codigo.Contains(filter.CodigoOuNome)
                    || t.Nome.Contains(filter.CodigoOuNome)))
            .OrderBy(t => t.Nome).ToList();
    }

    private static List<Temporada> TemporadasSemAno(
        ApplicationDbContext context,
        TemporadaFilter filter,
        Guid escolaId)
    {
        return context.Temporadas
            .Where(t => t.EscolaId == escolaId && t.Ano.HasValue == false
                && (filter.CodigoOuNome == null
                    || t.Codigo.Contains(filter.CodigoOuNome)
                    || t.Nome.Contains(filter.CodigoOuNome)))
            .OrderBy(t => t.Nome).ToList();
    }

    private static List<Temporada> TemporadasSemFiltroDeAno(
        ApplicationDbContext context,
        TemporadaFilter filter,
        Guid escolaId)
    {
        return context.Temporadas
            .Where(t => t.EscolaId == escolaId
                && (filter.CodigoOuNome == null
                    || t.Codigo.Contains(filter.CodigoOuNome)
                    || t.Nome.Contains(filter.CodigoOuNome)))
            .OrderBy(t => t.Nome).ToList();
    }
}
