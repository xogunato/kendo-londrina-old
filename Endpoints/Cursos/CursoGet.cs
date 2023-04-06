using Microsoft.EntityFrameworkCore;
using w_escolas.Domain.Cursos;
using w_escolas.Endpoints.Cursos.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Cursos;

public class CursoGet
{
    public static string Template => "/cursos";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        CursoFilter? filter,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var cursos = GetByFilter(context, filter!, escolaIdDoUsuarioCorrente);

        var response = cursos.Select(
            t => new CursoComTipoResponse
            {
                Id = t.Id,
                Codigo = t.Codigo,
                Nome = t.Nome,
                Ordem = t.Ordem,
                TipoDeCursoId = t.TipoDeCursoId,
                CodTipoDeCurso = t.TipoDeCurso!.Codigo,
                NomeTipoDeCurso = t.TipoDeCurso.Nome
            }
        );

        return Results.Ok(response);
    }

    private static List<Curso> GetByFilter(ApplicationDbContext context, CursoFilter filter, Guid escolaId)
    {
        if (filter == null)
            return GetAll(context, escolaId);

        if (filter.Id != null && filter.Id != "")
            return GetById(context, filter.Id);

        if (filter.TipoDeCursoId != null && filter.TipoDeCursoId != "")
            return GetByTipoDeCurso(context, filter.TipoDeCursoId);

        if (filter.CodigoOuNome != null)
            return GetByCodigoOuNome(context, filter.CodigoOuNome!, escolaId);

        return GetAll(context, escolaId);
    }

    private static List<Curso> GetAll(ApplicationDbContext context, Guid escolaId)
    {
        var ret = context.Cursos
            .Include(t => t.TipoDeCurso)
            .Where(t => t.EscolaId == escolaId)
            .OrderBy(t => t.TipoDeCurso!.Ordem).ThenBy(t => t.Ordem).ToList();
        return ret;
    }

    private static List<Curso> GetById(ApplicationDbContext context, string CursoId)
    {
        return context.Cursos
            .Include(t => t.TipoDeCurso)
            .Where(t => t.Id.ToString() == CursoId).ToList();
    }

    private static List<Curso> GetByTipoDeCurso(ApplicationDbContext context, string tipoDeCursoId)
    {
        return context.Cursos
            .Include(t => t.TipoDeCurso)
            .Where(t => t.TipoDeCursoId.ToString() == tipoDeCursoId)
            .OrderBy(t => t.TipoDeCurso!.Ordem).ThenBy(t => t.Ordem).ToList();
    }

    private static List<Curso> GetByCodigoOuNome(ApplicationDbContext context, string codigoOuNome, Guid escolaId)
    {
        var ret = context.Cursos
            .Include(t => t.TipoDeCurso)
            .Where(t => t.EscolaId == escolaId &&
                (t.Codigo.Contains(codigoOuNome) || t.Nome.Contains(codigoOuNome)))
            .OrderBy(t => t.TipoDeCurso!.Ordem).ThenBy(t => t.Ordem).ToList();
        return ret;
    }
}
