using w_escolas.Domain._abstractClasses;
using w_escolas.Domain.Escolas;
using w_escolas.Domain.Matriculas;
using w_escolas.Domain.TiposDeCursos;
using w_escolas.Domain.Turmas;

namespace w_escolas.Domain.Cursos;

public class Curso : Entity
{
    public Guid TipoDeCursoId { get; private set; }
    public string Codigo { get; private set; }
    public string Nome { get; private set; }
    public int Ordem { get; private set; }
    public Guid EscolaId { get; private set; }

    virtual public TipoDeCurso? TipoDeCurso { get; private set; }
    virtual public Escola? Escola { get; private set; }
    virtual public IEnumerable<Turma>? Turmas { get; private set; }
    virtual public IEnumerable<Matricula>? Matriculas { get; private set; }

    public Curso(Guid tipoDeCursoId, string codigo, string nome, int ordem, Guid escolaId)
    {
        TipoDeCursoId = tipoDeCursoId;
        Codigo = codigo;
        Nome = nome;
        Ordem = ordem;
        EscolaId = escolaId;
    }

    public void Alterar(Guid tipoDeCursoId, string codigo, string nome, int ordem)
    {
        TipoDeCursoId = tipoDeCursoId;
        Codigo = codigo;
        Nome = nome;
        Ordem = ordem;
    }
}
