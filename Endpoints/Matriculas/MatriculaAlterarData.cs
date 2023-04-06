using Microsoft.AspNetCore.Mvc;
using w_escolas.Domain.Matriculas;
using w_escolas.Endpoints.Matriculas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Matriculas;

public class MatriculaAlterarData
{
    public static string Template => "/matriculas/{id:guid}/alterar-data-matricula";
    public static string[] Methods => new string[] { HttpMethod.Patch.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id,
        DataMatriculaRequest matriculaRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var matricula = context.Matriculas.Where(e => e.Id == id).FirstOrDefault();
        if (matricula == null)
            return Results.NotFound();

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();
        if (matricula.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        matricula.AlterarDataMatricula(matriculaRequest.DataMatricula);
        var validator = new MatriculaValidator();
        var validation = validator.Validate(matricula);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        context.Matriculas.Update(matricula);
        context.SaveChanges();
        return Results.Ok();
    }
}
