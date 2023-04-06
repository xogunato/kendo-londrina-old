using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using w_escolas.Infra.Data;

namespace w_escolas.Endpoints.Escolas;

public class EscolaDelete
{
    public static string Template => "/escolas/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "AdminPolicy")]
    public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var escola = context.Escolas.Where(e => e.Id == id).FirstOrDefault();
        if (escola == null)
        {
            return Results.NotFound();
        }

        context.Escolas.Remove(escola);
        context.SaveChanges();

        return Results.Ok();
    }
}
