using Microsoft.AspNetCore.Mvc;
using w_escolas.Domain.Escolas;
using w_escolas.Endpoints.Escolas.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Escolas;

public class EscolaPut
{
    public static string Template => "/escolas/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id,
        EscolaRequest escolaRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();
        if (id != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        var escola = context.Escolas.Where(e => e.Id == id).FirstOrDefault();
        if (escola == null)
            return Results.NotFound();

        escola.Alterar(escolaRequest.NomeFantasia!,
            escolaRequest.Uf!,
            escolaRequest.Cidade!,
            escolaRequest.Cnpj!,
            escolaRequest.RazaoSocial!,
            escolaRequest.Cep!,
            escolaRequest.Bairro!,
            escolaRequest.Endereco!,
            escolaRequest.Telefone!,
            escolaRequest.Email!,
            escolaRequest.Website!
        );
        var validator = new EscolaValidator();
        var validation = validator.Validate(escola);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        context.Escolas.Update(escola);
        context.SaveChanges();
        return Results.Ok();
    }
}
