using Microsoft.AspNetCore.Mvc;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Matriculas;

public class MatriculaDelete
{
    public static string Template => "/Matriculas/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        [FromRoute] Guid id,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var matricula = context.Matriculas.Where(e => e.Id == id).FirstOrDefault();
        if (matricula == null)
            return Results.NotFound();

        if (matricula.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        //if (NaoPodeExcluir(context, id))
        //    return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());
        // TODO: verificar
        // - se está enturmado
        // - se tem parcelas financeiras
        // - se tem notas/ocorrências

        context.Matriculas.Remove(matricula);
        context.SaveChanges();

        return Results.Ok();
    }
}
