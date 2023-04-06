using Microsoft.EntityFrameworkCore;
using w_escolas.Domain.Turmas;
using w_escolas.Endpoints.Turmas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Turmas;

public class TurmaGet
{
    public static string Template => "/turmas";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        TurmaFilter? filter,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var turmas = GetByFilter(context, filter!, escolaIdDoUsuarioCorrente);

        var response = turmas.Select(
            t => new TurmaComCursoResponse
            {
                Id = t.Id,
                Codigo = t.Codigo,
                Nome = t.Nome,
                Ordem = t.Ordem,
                MaxAlunos = t.MaxAlunos,
                DataInicial = t.DataInicial,
                DataFinal = t.DataFinal,
                CursoId = t.CursoId,
                CodCurso = t.Curso!.Codigo,
                NomeCurso = t.Curso.Nome
            }
        );

        return Results.Ok(response);
    }

    private static List<Turma> GetByFilter(ApplicationDbContext context, TurmaFilter filter, Guid escolaId)
    {
        if (filter == null)
            return GetAll(context, escolaId);

        if (filter.Id != null && filter.Id != "")
            return GetById(context, filter.Id);

        if (filter.CursoId != null && filter.CursoId != "")
            return GetByCurso(context, filter.CursoId);

        if (filter.CodigoOuNome != null)
            return GetByCodigoOuNome(context, filter.CodigoOuNome!, escolaId);

        return GetAll(context, escolaId);
    }

    private static List<Turma> GetAll(ApplicationDbContext context, Guid escolaId)
    {
        var ret = context.Turmas
            .Include(t => t.Curso)
            .Where(t => t.EscolaId == escolaId)
            .OrderBy(t => t.Curso!.Ordem).ThenBy(t => t.Ordem).ToList();
        return ret;
    }

    private static List<Turma> GetById(ApplicationDbContext context, string turmaId)
    {
        return context.Turmas
            .Include(t => t.Curso)
            .Where(t => t.Id.ToString() == turmaId).ToList();
    }

    private static List<Turma> GetByCurso(ApplicationDbContext context, string cursoId)
    {
        return context.Turmas
            .Include(t => t.Curso)
            .Where(t => t.CursoId.ToString() == cursoId)
            .OrderBy(t => t.Curso!.Ordem).ThenBy(t => t.Ordem).ToList();
    }

    private static List<Turma> GetByCodigoOuNome(ApplicationDbContext context, string codigoOuNome, Guid escolaId)
    {
        var ret = context.Turmas
            .Include(t => t.Curso)
            .Where(t => t.EscolaId == escolaId &&
                (t.Codigo.Contains(codigoOuNome) || t.Nome.Contains(codigoOuNome)))
            .OrderBy(t => t.Curso!.Ordem).ThenBy(t => t.Ordem).ToList();
        return ret;
    }
}
