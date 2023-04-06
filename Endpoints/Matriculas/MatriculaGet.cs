using Microsoft.EntityFrameworkCore;
using w_escolas.Domain.Alunos;
using w_escolas.Domain.Escolas;
using w_escolas.Domain.Matriculas;
using w_escolas.Endpoints.Alunos.dtos;
using w_escolas.Endpoints.Matriculas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Matriculas;

public class MatriculaGet
{
    public static string Template => "/matriculas";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        MatriculaFilter? filter,
        ApplicationDbContext context,
        UserInfo userInfo,
        int page = 1, int row = 10, string orderBy = "Curso", string sortOrder = "asc")
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var queryBase = context.Matriculas.AsNoTracking()
            .Include(t => t.Curso)
            .Include(t => t.Aluno)
            .Include(t => t.Temporada)
            .Where(t => t.EscolaId == escolaIdDoUsuarioCorrente);

        IQueryable<Matricula> queryOrder = orderBy.ToLower() switch
        {
            "curso" => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.Curso!.Ordem)
                : queryBase.OrderByDescending(t => t.Curso!.Ordem),
            "aluno" => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.Aluno!.Nome)
                : queryBase.OrderByDescending(t => t.Aluno!.Nome),
            _ => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.Curso!.Ordem).ThenBy(t => t.Aluno!.Nome)
                : queryBase.OrderByDescending(t => t.Curso!.Ordem).ThenByDescending(t => t.Aluno!.Nome)
        };
        var queryFiltered = ApplyFilter(queryOrder, filter);

        var queryPaginated = queryFiltered.Skip((page - 1) * row).Take(row);

        var matriculasNew = queryPaginated.ToList();
        var responseNew = matriculasNew.Select(
            t => new MatriculaResponse
            {
                Id = t.Id,
                CursoId = t.CursoId,
                AlunoId = t.AlunoId,
                TemporadaId = t.TemporadaId,
                DataMatricula = t.DataMatricula,
                Cancelada = t.Cancelada,
                Curso = t.Curso,
                Aluno = t.Aluno,
                Temporada = t.Temporada
            }
        );

        var matriculas = Get(context, filter!, escolaIdDoUsuarioCorrente);
        var response = matriculas.Select(
            t => new MatriculaResponse
            {
                Id = t.Id,
                CursoId = t.CursoId,
                AlunoId = t.AlunoId,
                TemporadaId = t.TemporadaId,
                DataMatricula = t.DataMatricula,
                Cancelada = t.Cancelada
            }
        );
        return Results.Ok(responseNew);
    }

    private static IQueryable<Matricula> ApplyFilter(IQueryable<Matricula> inputQuery, MatriculaFilter? filter)
    {
        if (filter == null)
            return inputQuery;

        if (filter.Id != null && filter.Id != "")
            return inputQuery.Where(t => t.Id.ToString() == filter.Id);
        else if (filter.AlunoId != null)
            return inputQuery.Where(t => t.AlunoId.ToString() == filter.AlunoId);
        else if (filter.CursoId != null)
            return inputQuery.Where(t => t.CursoId.ToString() == filter.CursoId);
        else if (filter.TemporadaId != null)
            return inputQuery.Where(t => t.TemporadaId.ToString() == filter.TemporadaId);

        return inputQuery;
    }

    private static List<Matricula> Get(
        ApplicationDbContext context,
        MatriculaFilter filter,
        Guid escolaId)
    {
        if (filter == null)
            return GetAll(context, escolaId);

        if (filter.Id != null && filter.Id != "")
            return GetById(context, filter.Id);

        if (filter.TemporadaId != null && filter.TemporadaId != "")
            return GetByTemporada(context, filter.TemporadaId);

        return GetAll(context, escolaId);
    }

    private static List<Matricula> GetAll(ApplicationDbContext context, Guid escolaId)
    {
        return context.Matriculas
            .Where(t => t.EscolaId == escolaId)
            .OrderBy(t => t.DataMatricula).ToList();
    }
    private static List<Matricula> GetById(ApplicationDbContext context, string id)
    {
        return context.Matriculas
            .Where(t => t.Id.ToString() == id).ToList();
    }
    private static List<Matricula> GetByTemporada(
        ApplicationDbContext context,
        string temporadaId)
    {
        return context.Matriculas
            .Where(t => t.TemporadaId.ToString() == temporadaId)
            .OrderBy(t => t.DataMatricula).ToList();
    }

}
