using w_escolas.Domain._abstractClasses;
using w_escolas.Domain.Alunos;
using w_escolas.Domain.Cursos;
using w_escolas.Domain.Escolas;
using w_escolas.Domain.Temporadas;

namespace w_escolas.Domain.Matriculas;

public class Matricula : Entity
{
    public Guid EscolaId { get; private set; }
    public Guid CursoId { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid TemporadaId { get; private set; }
    public DateTime DataMatricula { get; private set; }
    public bool Cancelada { get; private set; }

    virtual public Escola? Escola { get; private set; }
    virtual public Curso? Curso { get; private set; }
    virtual public Aluno? Aluno { get; private set; }
    virtual public Temporada? Temporada { get; private set; }

    public Matricula(
        Guid escolaId,
        Guid cursoId,
        Guid alunoId,
        Guid temporadaId,
        DateTime dataMatricula)
    {
        EscolaId = escolaId;
        CursoId = cursoId;
        AlunoId = alunoId;
        TemporadaId = temporadaId;
        DataMatricula = dataMatricula;
        Cancelada = false;
    }

    public void AlterarDataMatricula(DateTime dataMatricula)
    {
        DataMatricula = dataMatricula;
    }

    public void Cancelar()
    {
        Cancelada = true;
    }
}
