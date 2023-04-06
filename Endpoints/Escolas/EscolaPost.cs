using Microsoft.AspNetCore.Authorization;
using w_escolas.Domain.Escolas;
using w_escolas.Endpoints.Escolas.dtos;
using w_escolas.Infra.Data;

namespace w_escolas.Endpoints.Escolas;

public class EscolaPost
{
    public static string Template => "/escolas";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "AdminPolicy")]
    public static IResult Action(EscolaRequest escolaRequest, ApplicationDbContext context)
    {
        var escola = DtoToObj(escolaRequest);
        var validator = new EscolaValidator();
        var validation = validator.Validate(escola);
        if(!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        context.Escolas.Add(escola);
        context.SaveChanges();
        return Results.Created($"{Template}/{escola.Id}", escola.Id);
    }

    private static Escola DtoToObj(EscolaRequest escolaRequest)
    {
        return new Escola(
            escolaRequest.NomeFantasia!,
            escolaRequest.Uf!,
            escolaRequest.Cidade!,
            escolaRequest.Cnpj!,
            escolaRequest.RazaoSocial!,
            escolaRequest.Cep!,
            escolaRequest.Bairro!,
            escolaRequest.Endereco!,
            escolaRequest.Telefone!,
            escolaRequest.Email!,
            escolaRequest.Website!);
    }
}
