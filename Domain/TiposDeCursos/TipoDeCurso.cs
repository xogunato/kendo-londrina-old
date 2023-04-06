using w_escolas.Domain._abstractClasses;
using w_escolas.Domain.Cursos;
using w_escolas.Domain.Escolas;

namespace w_escolas.Domain.TiposDeCursos;

public class TipoDeCurso : Entity
{
    public Guid EscolaId { get; private set; }
    public string Codigo { get; private set; }
    public string Nome { get; private set; }
    public int Ordem { get; private set; }

    virtual public Escola? Escola { get; private set; }
    virtual public IEnumerable<Curso>? Cursos { get; private set; }

    public TipoDeCurso(Guid escolaId, string codigo, string nome, int ordem)
    {
        EscolaId = escolaId;
        Codigo = codigo;
        Nome = nome;
        Ordem = ordem;
    }

    public void Alterar(string codigo, string nome, int ordem)
    {
        Codigo = codigo;
        Nome = nome;
        Ordem = ordem;
    }
}
