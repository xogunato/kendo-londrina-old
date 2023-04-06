using w_escolas.Domain._abstractClasses;
using w_escolas.Domain.Enderecos;
using w_escolas.Domain.Escolas;

namespace w_escolas.Domain.Alunos;

public class ResponsavelPeloAluno : Pessoa
{
    public Guid EscolaId { get; private set; }
    virtual public Escola? Escola { get; private set; }
    public Guid AlunoId { get; private set; }
    virtual public Aluno? Aluno { get; private set; }
    public Guid? EnderecoId { get; private set; }
    virtual public Endereco? Endereco { get; private set; }

    public ResponsavelPeloAluno(Guid escolaId,
        string nome,
        string? codigo,
        DateTime? dataNascimento,
        string? nacionalidade,
        string? ufNascimento,
        string? cidadeNascimento,
        string? sexo,
        string? rg,
        string? cpf,
        string? email,
        string? telCelular,
        string? religiao) : base(
            nome, dataNascimento, nacionalidade, ufNascimento, cidadeNascimento,
            sexo, rg, cpf, email, telCelular, religiao)
    {
        this.EscolaId = escolaId;
    }
}
