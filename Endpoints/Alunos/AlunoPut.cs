using Microsoft.AspNetCore.Mvc;
using w_escolas.Domain.Alunos;
using w_escolas.Endpoints.Alunos.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Alunos;

public class AlunoPut
{
    public static string Template => "/alunos/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action([FromRoute] Guid id,
        AlunoRequest alunoRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var aluno = context.Alunos.Where(e => e.Id == id).FirstOrDefault();
        if (aluno == null)
            return Results.NotFound();

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();
        if (aluno.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        aluno.Alterar(
            alunoRequest.Nome!,
            alunoRequest.Codigo!.Trim(),
            alunoRequest.DataNascimento,
            alunoRequest.Nacionalidade!.Trim(),
            alunoRequest.UfNascimento!.Trim(),
            alunoRequest.CidadeNascimento!.Trim(),
            alunoRequest.Sexo!.Trim(),
            alunoRequest.Rg!.Trim(),
            alunoRequest.Cpf!.Trim(),
            alunoRequest.Email!.Trim(),
            alunoRequest.TelCelular!.Trim(),
            alunoRequest.Religiao!.Trim()
        );
        var validator = new AlunoValidator();
        var validation = validator.Validate(aluno);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeAlterar(context, aluno))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Alunos.Update(aluno);
        context.SaveChanges();
        return Results.Ok();
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoCodigo(ApplicationDbContext context, Aluno aluno)
    {
        if (aluno.Codigo is not null && aluno.Codigo != "")
        {
            if (context.Alunos.Where(t =>
                t.EscolaId == aluno.EscolaId &&
                t.Codigo == aluno.Codigo &&
                t.Id != aluno.Id).Any())
            {
                errorMessages.Add($"Já existe Aluno com código {aluno.Codigo}.");
            }
        }
    }

    private static bool NaoPodeAlterar(ApplicationDbContext context, Aluno aluno)
    {
        errorMessages.Clear();
        VerificarComMesmoCodigo(context, aluno);
        // VerificarComMesmoNome(context, aluno);
        return errorMessages.Count > 0;
    }

}
