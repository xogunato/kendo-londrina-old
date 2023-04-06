using System.Runtime.InteropServices;
using w_escolas.Domain._abstractClasses;
using w_escolas.Domain.Alunos;
using w_escolas.Domain.Cursos;
using w_escolas.Domain.Matriculas;
using w_escolas.Domain.Temporadas;
using w_escolas.Domain.TiposDeCursos;
using w_escolas.Domain.Turmas;

namespace w_escolas.Domain.Escolas;

public class Escola : Entity
{
    public string NomeFantasia { get; private set; }
    public string? Cnpj { get; private set; }
    public string? RazaoSocial { get; private set; }
    public string? Cep { get; private set; }
    public string Uf { get; private set; }
    public string Cidade { get; private set; }
    public string? Bairro { get; private set; }
    public string? Endereco { get; private set; }
    public string? Telefone { get; private set; }
    public string? Email { get; private set; }
    public string? Website { get; private set; }

    virtual public IEnumerable<TipoDeCurso>? TiposDeCursos { get; private set; }
    virtual public IEnumerable<Curso>? Cursos { get; private set; }
    virtual public IEnumerable<Turma>? Turmas { get; private set; }
    virtual public IEnumerable<Aluno>? Alunos { get; private set; }
    virtual public IEnumerable<Temporada>? Temporadas { get; private set; }
    virtual public IEnumerable<Matricula>? Matriculas { get; private set; }

    public Escola(string nomeFantasia, string uf, string cidade)
    {
        NomeFantasia = nomeFantasia;
        Uf = uf;
        Cidade = cidade;
    }
    public Escola(string nomeFantasia, string uf, string cidade,
        [Optional] string cnpj,
        [Optional] string razaoSocial,
        [Optional] string cep,
        [Optional] string bairro,
        [Optional] string endereco,
        [Optional] string telefone,
        [Optional] string email,
        [Optional] string website)
    {
        NomeFantasia = nomeFantasia;
        Uf = uf;
        Cidade = cidade;
        Cnpj = cnpj;
        RazaoSocial = razaoSocial;
        Cep = cep;
        Bairro = bairro;
        Endereco = endereco;
        Telefone = telefone;
        Email = email;
        Website = website;
    }

    public void Alterar(string nomeFantasia, string uf, string cidade,
        [Optional] string cnpj,
        [Optional] string razaoSocial,
        [Optional] string cep,
        [Optional] string bairro,
        [Optional] string endereco,
        [Optional] string telefone,
        [Optional] string email,
        [Optional] string website)
    {
        NomeFantasia = nomeFantasia;
        Uf = uf;
        Cidade = cidade;
        Cnpj = cnpj;
        RazaoSocial = razaoSocial;
        Cep = cep;
        Bairro = bairro;
        Endereco = endereco;
        Telefone = telefone;
        Email = email;
        Website = website;
    }
}
