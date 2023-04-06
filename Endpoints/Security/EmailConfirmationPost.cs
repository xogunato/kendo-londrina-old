using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;
using w_escolas.Domain.Escolas;

namespace w_escolas.Endpoints.Security;

public record EmailConfirmationRequest(string UserId, string Code);

public class EmailConfirmationPost
{
    public static string Template => "/email-confirmation";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(
        EmailConfirmationRequest confirmacaoDeEmail,
        UserManager<IdentityUser> userManager,
        ApplicationDbContext context)
    {
        if (confirmacaoDeEmail.UserId == null || confirmacaoDeEmail.Code == null)
            return Results.ValidationProblem("Informações necessárias não fornecidas".ConvertToProblemDetails());

        var user = await userManager.FindByIdAsync(confirmacaoDeEmail.UserId);
        if (user == null)
            return Results.ValidationProblem("Problemas com as informações fornecidas".ConvertToProblemDetails());

        if (user.EmailConfirmed)
            return Results.Ok("Email JÁ confirmado");

        var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmacaoDeEmail.Code));
        var result = await userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        Escola escola;
        try
        {
            escola = await CriarEscolaAsync(confirmacaoDeEmail.UserId, context);
        }
        catch (Exception ex)
        {
            return Results.ValidationProblem(ex.Message.ConvertToProblemDetails());
        }

        if (escola != null)
        {
            var claim = new Claim("EscolaId", escola.Id.ToString());
            var claimResult = userManager.AddClaimAsync(user, claim).Result;
            if (!claimResult.Succeeded)
                return Results.ValidationProblem(claimResult.Errors.ConvertToProblemDetails());
        }
        return Results.Ok("Obrigado por confirmar seu e-mail");
    }

    private static async Task<Escola> CriarEscolaAsync(string userId, ApplicationDbContext context)
    {
        var escola = new Escola(
            $"escola de {userId}",
            "NI", "NI"
        );
        context.Escolas.Add(escola);
        await context.SaveChangesAsync();

        return escola;
    }
}
