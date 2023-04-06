using Microsoft.AspNetCore.Mvc;
using w_escolas.Domain.TiposDeCursos;
using w_escolas.Endpoints.TiposDeCursos.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.TiposDeCursos;

public class TipoDeCursoGet
{
    public static string Template => "/tiposdecursos";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        TipoDeCursoFilter? filter,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var tiposDeCursos = Get(context, filter!, escolaIdDoUsuarioCorrente);

        var response = tiposDeCursos.Select(
            t => new TipoDeCursoResponse
            {
                Id = t.Id,
                Codigo = t.Codigo,
                Nome = t.Nome,
                Ordem = t.Ordem
            }
        );

        return Results.Ok(response);
    }

    private static List<TipoDeCurso> Get(ApplicationDbContext context, TipoDeCursoFilter filter, Guid escolaId)
    {
        if (filter == null)
            return GetAll(context, escolaId);

        if (filter.Id != null && filter.Id != "")
            return GetById(context, filter.Id);

        if (filter.CodigoOuNome != null && filter.CodigoOuNome != "")
            return GetByCodigoOuNome(context, filter.CodigoOuNome, escolaId);

        return GetAll(context, escolaId);
    }

    private static List<TipoDeCurso> GetAll(ApplicationDbContext context, Guid escolaId)
    {
        return context.TiposDeCursos
            .Where(t => t.EscolaId == escolaId)
            .OrderBy(t => t.Ordem).ToList();
    }
    private static List<TipoDeCurso> GetById(ApplicationDbContext context, string tipoDeCursoId)
    {
        return context.TiposDeCursos
            .Where(t => t.Id.ToString() == tipoDeCursoId).ToList();
    }
    private static List<TipoDeCurso> GetByCodigoOuNome(ApplicationDbContext context, string codigoOuNome, Guid escolaId)
    {
        return context.TiposDeCursos
            .Where(t => t.EscolaId == escolaId &&
                (t.Codigo.Contains(codigoOuNome) || t.Nome.Contains(codigoOuNome)) )
            .OrderBy(t => t.Ordem).ToList();
    }
}
