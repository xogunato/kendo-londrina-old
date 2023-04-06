using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace w_escolas.Endpoints.Usuarios;

public record LoginRequest(string Email, string Password);

public class TokenPost
{
    public static string Template => "/token";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => ActionAsync;

    [AllowAnonymous]
    public static async Task<IResult> ActionAsync(
        LoginRequest loginRequest,
        UserManager<IdentityUser> userManager,
        IConfiguration configuration)
    {
        var user = await userManager.FindByEmailAsync(loginRequest.Email);
        if (user == null)
            return Results.BadRequest("problema na autenticação/confirmação da conta");

        if (!user.EmailConfirmed)
            return Results.BadRequest("problema na autenticação/confirmação da conta");

        var passwordCheck = await userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!passwordCheck)
            return Results.BadRequest("problema na autenticação/confirmação da conta");

        var claims = await userManager.GetClaimsAsync(user);
        var escolaId = claims.FirstOrDefault((claim) => claim.Type == "EscolaId")!.Value;

        var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, loginRequest.Email),
                new Claim("EscolaId", escolaId),
            }),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration["JwtBearerTokenSettings:Audience"],
            Issuer = configuration["JwtBearerTokenSettings:Issuer"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}
