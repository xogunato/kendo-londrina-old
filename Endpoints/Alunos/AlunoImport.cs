using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using System.Net;
using w_escolas.Domain.Alunos;
using w_escolas.Endpoints.Alunos.dtos;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace w_escolas.Endpoints.Alunos;

public class AlunoImport
{
    public static string Template => "/alunos/import";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(
        HttpRequest request,
        // [FromBody] IFormFile file,
        // AlunoImportRequest alunoImportRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        //Console.WriteLine(">>>>>>>>>>");
        //Console.WriteLine(request.Form.Files.Count.ToString());
        //Console.WriteLine("<<<<<<<<<<");

        var file = request.Form.Files[0];
        var read = file.OpenReadStream();

        var fileContent = "";
        var cont = 0;
        using (var reader = new StreamReader(read, System.Text.Encoding.UTF8))
        {
            // Read the raw file as a `string`.
            // fileContent = await reader.ReadToEndAsync();

            // Do something with `fileContent`...
            //Console.WriteLine(">>>>>>>>>>");
            //Console.WriteLine(fileContent);
            //Console.WriteLine("<<<<<<<<<<");

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();
                Console.WriteLine(">>>>>>>>>>");
                Console.WriteLine(line);
                Console.WriteLine("<<<<<<<<<<");

                var columns = line!.Split(';');
                Console.WriteLine(">>>>>>>>>>");
                Console.WriteLine(columns.Length);
                Console.WriteLine("<<<<<<<<<<");

                if (line != "codAluno;nome;nascData;nacionalidade;nascUF;nascLocal;sexo;rg;cpf;email;telCelular;nome")
                {
                    var aluno = MakeAluno(escolaIdDoUsuarioCorrente, columns);
                    Console.WriteLine(">>>>>>>>>>");
                    Console.WriteLine(aluno.DataNascimento);
                    Console.WriteLine("<<<<<<<<<<");

                    if (!context.Alunos.Where(t =>
                                    t.Codigo == aluno.Codigo &&
                                    t.EscolaId == aluno.EscolaId).Any())
                    {
                        context.Alunos.Add(aluno);
                        cont++;
                    }
                }
            }
            context.SaveChanges();

        }

        return Results.Ok(cont.ToString());
    }

    private static Aluno MakeAluno(Guid escolaId, string[] alunoInfo)
    {
        return new Aluno(escolaId,
            alunoInfo[1].Trim(),
            alunoInfo[0].Trim(),
            Convert.ToDateTime(alunoInfo[2].Trim()),
            alunoInfo[3].Trim(),
            alunoInfo[4].Trim(),
            alunoInfo[5].Trim(),
            alunoInfo[6].Trim(),
            alunoInfo[7].Trim(),
            alunoInfo[8].Trim(),
            alunoInfo[9].Trim(),
            alunoInfo[10].Trim(),
            alunoInfo[11].Trim()
        );
    }

}
