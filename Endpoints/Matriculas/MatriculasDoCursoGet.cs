using Microsoft.AspNetCore.Mvc;
using w_escolas.Infra.Data;
using w_escolas.Infra.Data.DapperQueries.Matriculas;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Matriculas;

public class MatriculasDoCursoGet
{
    public static string Template => "/cursos/{cursoId:guid}/matriculas";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid cursoId,
        ApplicationDbContext context,
        UserInfo userInfo,
        MatriculasDoCursoQuery matriculasDoCurso,
        int page = 1, int row = 10, string orderBy = "Curso", string sortOrder = "asc")
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        return Results.Ok(matriculasDoCurso.Get(
            escolaIdDoUsuarioCorrente.ToString(),
            cursoId.ToString()));
    }

}
