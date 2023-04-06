using Microsoft.EntityFrameworkCore;
using w_escolas.Domain.TiposDeCursos;
using w_escolas.Endpoints.Turmas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Turmas;

public class ArvoreDeTurmaGet
{
    public static string Template => "/arvore-de-turmas";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var tiposCursosTurmas = ObterTiposCursosTurmas(context, escolaIdDoUsuarioCorrente);

        var arvore = new List<TipoDeCursoTreenode>();
        foreach (var tipoDeCurso in tiposCursosTurmas)
        {
            var cursos = new List<CursoTreenode>();
            if (tipoDeCurso.Cursos is not null)
            {
                foreach (var curso in tipoDeCurso.Cursos)
                {
                    var turmas = new List<TurmaTreenode>();
                    if (curso.Turmas is not null)
                    {
                        foreach (var turma in curso.Turmas)
                        {
                            turmas.Add(new TurmaTreenode
                            {
                                Id = turma.Id,
                                Codigo = turma.Codigo,
                                Nome = turma.Nome,
                                Ordem = turma.Ordem
                            });
                        }
                    }
                    cursos.Add(new CursoTreenode
                    {
                        Id = curso.Id,
                        Codigo = curso.Codigo,
                        Nome = curso.Nome,
                        Ordem = curso.Ordem,
                        Turmas = turmas
                    });
                }
            }
            arvore.Add(new TipoDeCursoTreenode
            {
                Id = tipoDeCurso.Id,
                Codigo = tipoDeCurso.Codigo,
                Nome = tipoDeCurso.Nome,
                Ordem = tipoDeCurso.Ordem,
                Cursos = cursos
            });
        }

        return Results.Ok(arvore);
    }

    private static List<TipoDeCurso> ObterTiposCursosTurmas(ApplicationDbContext context, Guid escolaId)
    {
        var ret = context.TiposDeCursos
            .Include(t => t.Cursos!.OrderBy(curso => curso.Ordem))
            .ThenInclude(c => c.Turmas)
            .Where(t => t.EscolaId == escolaId)
            .OrderBy(t => t.Ordem).ToList();
        return ret;
    }

}
