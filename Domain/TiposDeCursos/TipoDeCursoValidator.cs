using FluentValidation;

namespace w_escolas.Domain.TiposDeCursos;

public class TipoDeCursoValidator : AbstractValidator<TipoDeCurso>
{
    public TipoDeCursoValidator()
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
