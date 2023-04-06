using System.Runtime.Intrinsics.Arm;
using w_escolas.Domain._abstractClasses;
using w_escolas.Domain.Cursos;
using w_escolas.Domain.Escolas;
using w_escolas.Domain.Matriculas;

namespace w_escolas.Domain.Temporadas;

public class Temporada : Entity
{
    public Guid EscolaId { get; private set; }
    public string Codigo { get; private set; }
    public string Nome { get; private set; }
    public int? Ano { get; private set; }
    public int? Semestre { get; private set; }
    public int? Quadrimestre { get; private set; }
    public int? Trimestre { get; private set; }
    public int? Bimestre { get; private set; }
    public int? Mes { get; private set; }

    virtual public Escola? Escola { get; private set; }
    virtual public IEnumerable<Curso>? Cursos { get; private set; }
    virtual public IEnumerable<Matricula>? Matriculas { get; private set; }

    public Temporada(Guid escolaId, string codigo, string nome,
        int? ano, int? semestre, int? quadrimestre, int? trimestre, int? bimestre, int? mes)
    {
        EscolaId = escolaId;
        Codigo = codigo;
        Nome = nome;
        Ano = ano;
        Semestre = semestre;
        Quadrimestre = quadrimestre;
        Trimestre = trimestre;
        Bimestre = bimestre;
        Mes = mes;
    }
    public void Alterar(string codigo, string nome,
        int? ano, int? semestre, int? quadrimestre, int? trimestre, int? bimestre, int? mes)
    {
        Codigo = codigo;
        Nome = nome;
        Ano = ano;
        Semestre = semestre;
        Quadrimestre = quadrimestre;
        Trimestre = trimestre;
        Bimestre = bimestre;
        Mes = mes;
    }
}
