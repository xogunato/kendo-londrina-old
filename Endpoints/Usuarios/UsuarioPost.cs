using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

namespace w_escolas.Endpoints.Usuarios;

public record UsuarioDto(
    string Email,
    string Password,
    string NomeDoUsuario,
    string Cpf);

public class UsuarioPost
{
    public static string Template => "/usuarios";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(
        UsuarioDto usuarioRequest,
        UserManager<IdentityUser> userManager,
        IEmailSender emailSender,
        IConfiguration configuration)
    {
        var user = new IdentityUser { UserName = usuarioRequest.Email, Email = usuarioRequest.Email };
        var result = userManager.CreateAsync(user, usuarioRequest.Password).Result;

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        var claims = new List<Claim>
        {
            new Claim("Cpf", usuarioRequest.Cpf),
            new Claim("NomeDoUsuario", usuarioRequest.NomeDoUsuario)
            // new Claim("OutraClaim", OutraClaim)
            // ...
        };

        var claimResult = userManager.AddClaimsAsync(user, claims).Result;
        if (!claimResult.Succeeded)
            return Results.ValidationProblem(claimResult.Errors.ConvertToProblemDetails());

        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var confirmationLink = $"{configuration["Url:FrontEnd:Root"]}" +
            $"/{configuration["Url:FrontEnd:AccountConfirmation"]}" +
            $"/{user.Id}/{code}";

        await emailSender.SendEmailAsync(user.Email, "Nova conta MeuSistemaEscolar",
            $"Confirme sua conta <a href='{confirmationLink}'>clicando aqui</a>.");

        return Results.Created($"/usuarios/{user.Id}", confirmationLink);
    }
}
