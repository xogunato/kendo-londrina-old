using w_escolas.Domain._abstractClasses;
using w_escolas.Domain.Cursos;
using w_escolas.Domain.Escolas;

namespace w_escolas.Domain.Turmas;

public class Turma : Entity
{
    public Guid EscolaId { get; private set; }
    public Guid CursoId { get; private set; }
    public string Codigo { get; private set; }
    public string Nome { get; private set; }
    public int Ordem { get; private set; }
    public int? MaxAlunos { get; private set; }
    public DateTime? DataInicial { get; private set; }
    public DateTime? DataFinal { get; private set; }

    virtual public Curso? Curso { get; private set; }
    virtual public Escola? Escola { get; private set; }

    public Turma(
        Guid escolaId,
        Guid cursoId,
        string codigo,
        string nome,
        int ordem,
        int? maxAlunos,
        DateTime? dataInicial,
        DateTime? dataFinal)
    {
        EscolaId = escolaId;
        CursoId = cursoId;
        Codigo = codigo;
        Nome = nome;
        Ordem = ordem;
        MaxAlunos = maxAlunos ?? null;
        DataInicial = dataInicial ?? null;
        DataFinal = dataFinal ?? null;
    }

    public void Alterar(
        Guid cursoId,
        string codigo,
        string nome,
        int ordem,
        int? maxAlunos,
        DateTime? dataInicial,
        DateTime? dataFinal)
    {
        CursoId = cursoId;
        Codigo = codigo;
        Nome = nome;
        Ordem = ordem;
        MaxAlunos = maxAlunos;
        DataInicial = dataInicial;
        DataFinal = dataFinal;
    }
}
