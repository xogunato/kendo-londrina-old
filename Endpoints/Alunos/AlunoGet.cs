using Microsoft.EntityFrameworkCore;
using w_escolas.Domain.Alunos;
using w_escolas.Endpoints.Alunos.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Alunos;

public class AlunoGet
{
    public static string Template => "/alunos";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        AlunoFilter? filter,
        ApplicationDbContext context,
        UserInfo userInfo,
        int page = 1, int row = 10, string orderBy = "Nome", string sortOrder = "asc")
    {
        if (row > 100)
            return Results.Problem(title: "Max row value must be 100", statusCode: 400);

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var queryBase = context.Alunos.AsNoTracking()
            .Where(t => t.EscolaId == escolaIdDoUsuarioCorrente);
        IQueryable<Aluno> queryOrder = orderBy.ToLower() switch
        {
            "codigo" => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.Codigo)
                : queryBase.OrderByDescending(t => t.Codigo),
            "datanascimento" => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.DataNascimento)
                : queryBase.OrderByDescending(t => t.DataNascimento),
            _ => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.Nome)
                : queryBase.OrderByDescending(t => t.Nome),
        };
        var queryFiltered = ApplyFilter(queryOrder, filter);

        var queryPaginated = queryFiltered.Skip((page - 1) * row).Take(row);

        var alunos = queryPaginated.ToList();

        var response = alunos.Select(
            t => new AlunoResponse
            {
                Id = t.Id,
                Codigo = t.Codigo,
                Nome = t.Nome,
                DataNascimento = t.DataNascimento,
                Nacionalidade = t.Nacionalidade,
                UFNascimento = t.UfNascimento,
                CidadeNascimento = t.CidadeNascimento,
                Sexo = t.Sexo,
                RG = t.Rg,
                CPF = t.Cpf,
                Email = t.Email,
                TelCelular = t.TelCelular,
                Religiao = t.Religiao
            }
        );

        if (filter != null && filter.Id != null && filter.Id != "")
            return Results.Ok(response);

        var pageDto = new PageDto<Aluno> { Count = queryFiltered.ToList().Count, Data = alunos };
        return Results.Ok(pageDto);
    }

    private static IQueryable<Aluno> ApplyFilter(IQueryable<Aluno> inputQuery, AlunoFilter? filter)
    {
        if (filter == null)
            return inputQuery;

        if (filter.Id != null && filter.Id != "")
            return inputQuery.Where(t => t.Id.ToString() == filter.Id);
        else if (filter.CodigoOuNome != null)
            return inputQuery.Where(t =>
                t.Codigo!.Contains(filter.CodigoOuNome) ||
                t.Nome!.Contains(filter.CodigoOuNome));
        return inputQuery;
    }

    //private static List<Aluno> GetByFilter(ApplicationDbContext context, AlunoFilter filter, Guid escolaId)
    //{
    //    if (filter == null)
    //        return GetAll(context, escolaId);

    //    if (filter.Id != null && filter.Id != "")
    //        return GetById(context, filter.Id);

    //    //if (filter.CursoId != null && filter.CursoId != "")
    //    //    return GetByCurso(context, filter.CursoId);

    //    if (filter.CodigoOuNome != null)
    //        return GetByCodigoOuNome(context, filter.CodigoOuNome!, escolaId);

    //    return GetAll(context, escolaId);
    //}

    //private static List<Aluno> GetAll(ApplicationDbContext context, Guid escolaId)
    //{
    //    var ret = context.Alunos
    //        .Where(t => t.EscolaId == escolaId)
    //        .OrderBy(t => t.Nome).ToList();
    //    return ret;
    //}

    //private static List<Aluno> GetById(ApplicationDbContext context, string alunoId)
    //{
    //    return context.Alunos
    //        .Where(t => t.Id.ToString() == alunoId).ToList();
    //}

    //private static List<Aluno> GetByCurso(ApplicationDbContext context, string cursoId)
    //{
    //    return context.Alunos
    //        .Include(t => t.TipoDeCurso)
    //        .Where(t => t.TipoDeCursoId.ToString() == tipoDeCursoId)
    //        .OrderBy(t => t.TipoDeCurso!.Ordem).ThenBy(t => t.Ordem).ToList();
    //}

    //private static List<Aluno> GetByCodigoOuNome(ApplicationDbContext context, string codigoOuNome, Guid escolaId)
    //{
    //    var ret = context.Alunos
    //        .Where(t => t.EscolaId == escolaId &&
    //            (t.Codigo!.Contains(codigoOuNome) || t.Nome!.Contains(codigoOuNome)))
    //        .OrderBy(t => t.Nome).ToList();
    //    return ret;
    //}

}
