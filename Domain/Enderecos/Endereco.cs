using w_escolas.Domain._abstractClasses;

namespace w_escolas.Domain.Enderecos;

public class Endereco : Entity
{
    public string? Cep { get; private set; }
    public string? Uf { get; private set; }
    public string? Cidade { get; private set; }
    public string? Bairro { get; private set; }
    public string? Distrito { get; private set; }
    public string? Complemento { get; private set; }
    public string? Logradouro { get; private set; }
    public string? Tipo { get; private set; }

    public Endereco(string? cep, string? uf, string? cidade, string? bairro, string? distrito, string? complemento, string? logradouro, string? tipo)
    {
        Cep = cep;
        Uf = uf;
        Cidade = cidade;
        Bairro = bairro;
        Distrito = distrito;
        Complemento = complemento;
        Logradouro = logradouro;
        Tipo = tipo;
    }

    public void Alterar()
    {

    }
}
