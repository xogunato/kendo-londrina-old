using FluentValidation;

namespace w_escolas.Domain.Turmas;

public class TurmaValidator : AbstractValidator<Turma>
{
    public TurmaValidator()
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
