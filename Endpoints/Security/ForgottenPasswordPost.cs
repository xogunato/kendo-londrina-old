using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace w_escolas.Endpoints.Security;

public record PasswordChangeDto(string Email, string ConfirmationUrl);

public class ForgottenPasswordPost
{
    public static string Template => "/forgotten-password";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(
        PasswordChangeDto passwordChangeDto,
        UserManager<IdentityUser> userManager,
        IEmailSender emailSender)
    {
        var user = await userManager.FindByEmailAsync(passwordChangeDto.Email);
        // Don't reveal that the user does not exist or is not confirmed
        if (user == null || !user.EmailConfirmed)
            return Results.Ok($"Email enviado para {passwordChangeDto.Email} ...");

        var code = await userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var link = $"{passwordChangeDto.ConfirmationUrl}/{user.Id}/{code}";
        try
        {
            await emailSender.SendEmailAsync(user.Email, "Redefinição de senha MeuSistemaEscolar",
                      $"Altere sua senha <a href='{link}'>clicando aqui</a>.");
            return Results.Ok($"Email enviado para {passwordChangeDto.Email} com sucesso.");
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
