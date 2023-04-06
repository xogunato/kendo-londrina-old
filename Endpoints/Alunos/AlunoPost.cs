using w_escolas.Domain.Alunos;
using w_escolas.Endpoints.Alunos.dtos;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Alunos;

public class AlunoPost
{
    public static string Template => "/alunos";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(
        AlunoRequest alunoRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var aluno = DtoToObj(escolaIdDoUsuarioCorrente, alunoRequest);
        var validator = new AlunoValidator();
        var validation = validator.Validate(aluno);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeIncluir(context, aluno))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Alunos.Add(aluno);
        context.SaveChanges();
        return Results.Created($"{Template}/{aluno.Id}", aluno.Id);
    }

    private static Aluno DtoToObj(Guid escolaId, AlunoRequest alunoRequest)
    {
        return new Aluno(escolaId,
            alunoRequest.Nome!.Trim(),
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
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoCodigo(ApplicationDbContext context, Aluno aluno)
    {
        if (aluno.Codigo is not null && aluno.Codigo != "")
        {
            if (context.Alunos.Where(t =>
                t.Codigo == aluno.Codigo &&
                t.EscolaId == aluno.EscolaId).Any())
            {
                errorMessages.Add($"Já existe Aluno com código {aluno.Codigo}.");
            }
        }
    }

    private static bool NaoPodeIncluir(ApplicationDbContext context, Aluno aluno)
    {
        errorMessages.Clear();
        VerificarComMesmoCodigo(context, aluno);
        //VerificarComMesmoNome(context, curso);
        return errorMessages.Count > 0;
    }

}
