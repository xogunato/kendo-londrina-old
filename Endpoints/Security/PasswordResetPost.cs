using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace w_escolas.Endpoints.Security;

public record PasswordResetDto(string UserId, string NewPassword, string ConfirmationCode);

public class PasswordResetPost
{
    public static string Template => "/password-reset";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(
        PasswordResetDto passwordResetDto,
        UserManager<IdentityUser> userManager,
        IEmailSender emailSender)
    {
        var user = await userManager.FindByIdAsync(passwordResetDto.UserId);
        // Don't reveal that the user does not exist or is not confirmed
        if (user == null || !user.EmailConfirmed)
            return Results.BadRequest("Senha não foi redefinida");

        var deCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(passwordResetDto.ConfirmationCode));
        try
        {
            var result = await userManager.ResetPasswordAsync(user, deCode, passwordResetDto.NewPassword);
            if (result.Succeeded)
                return Results.Ok("Senha foi redefinida com sucesso.");
            else
                return Results.BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
