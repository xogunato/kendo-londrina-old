namespace w_escolas.Domain._abstractClasses;

public abstract class Pessoa: Entity
{
    public string Nome { get; protected set; }
    public DateTime? DataNascimento { get; protected set; }
    public string? Nacionalidade { get; protected set; }
    public string? UfNascimento { get; protected set; }
    public string? CidadeNascimento { get; protected set; }
    public string? Sexo { get; protected set; }
    public string? Rg { get; protected set; }
    public string? Cpf { get; protected set; }
    public string? Email { get; protected set; }
    public string? TelCelular { get; protected set; }
    public string? Religiao { get; protected set; }

    public Pessoa(string nome,
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
