using Microsoft.AspNetCore.Authorization;
using w_escolas.Endpoints.Escolas.dtos;
using w_escolas.Infra.Data;

namespace w_escolas.Endpoints.Escolas;

public class EscolaGetAll
{
    public static string Template => "/escolas";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "AdminPolicy")]
    public static IResult Action(ApplicationDbContext context)
    {
        var escolas = context.Escolas.ToList();
        var response = escolas.Select(
            e => new EscolaResponse
            {
                Id = e.Id,
                NomeFantasia = e.NomeFantasia,
                Uf = e.Uf,
                Cidade = e.Cidade,
                Cnpj = e.Cnpj,
                RazaoSocial = e.RazaoSocial,
                Cep = e.Cep,
                Bairro = e.Bairro,
                Endereco = e.Endereco,
                Telefone = e.Telefone,
                Email = e.Email,
                Website = e.Website
            }
        );
        return Results.Ok(response);
    }
}
