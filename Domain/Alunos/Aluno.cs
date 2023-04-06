using w_escolas.Domain._abstractClasses;
using w_escolas.Domain.Enderecos;
using w_escolas.Domain.Escolas;
using w_escolas.Domain.Matriculas;

namespace w_escolas.Domain.Alunos;

public class Aluno : Pessoa
{
    public Guid EscolaId { get; private set; }
    virtual public Escola? Escola { get; private set; }
    public string? Codigo { get; private set; }
    public Guid? EnderecoId { get; private set; }

    virtual public Endereco? Endereco { get; private set; }
    virtual public IEnumerable<Matricula>? Matriculas { get; private set; }

    public Aluno(Guid escolaId,
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
            sexo, rg, cpf, email,telCelular, religiao)
    {
        this.EscolaId = escolaId;
        this.Codigo = codigo;
    }

    public void AlterarEndereco(Guid enderecoId, Endereco endereco)
    {
        this.EnderecoId = enderecoId;
        this.Endereco = endereco;
    }

    public void Alterar(
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
        string? religiao)
    {
        this.Nome = nome;
        this.Codigo = codigo;
        this.DataNascimento = dataNascimento;
        this.Nacionalidade = nacionalidade;
        this.UfNascimento = ufNascimento;
        this.CidadeNascimento = cidadeNascimento;
        this.Sexo = sexo;
        this.Rg = rg;
        this.Cpf = cpf;
        this.Email = email;
        this.TelCelular = telCelular;
        this.Religiao = religiao;
    }
}
