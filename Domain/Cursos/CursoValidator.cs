using FluentValidation;

namespace w_escolas.Domain.Cursos;

public class CursoValidator : AbstractValidator<Curso>
{
    public CursoValidator()
    {
        RuleFor(t => t.Codigo)
            .NotEmpty();
        RuleFor(t => t.Nome)
            .NotEmpty()
            .MinimumLength(3);
        RuleFor(t => t.Ordem)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}
