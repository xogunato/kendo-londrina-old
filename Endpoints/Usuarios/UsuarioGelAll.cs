using Microsoft.AspNetCore.Authorization;
using w_escolas.Infra.Data.DapperQueries;

namespace w_escolas.Endpoints.Usuarios;

public class UsuarioGetAll
{
    public static string Template => "/usuarios";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int page, int rows, QueryAllUsersWithClaimNomeDoUsuario query)
    {
        return Results.Ok(query.Execute(page, rows));
    }
    //    public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager)
    //    {
    //        var usuarios = userManager.Users
    //            .Skip((page - 1) * rows)
    //            .Take(rows)
    //            .ToList()
    //            .Select(u => new UsuarioResponse(u.Email, "pegar depois pleos claims"));
    //        return Results.Ok(usuarios);
    //    }
}
