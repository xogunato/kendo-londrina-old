using FluentValidation;

namespace w_escolas.Domain.Alunos;

public class AlunoValidator : AbstractValidator<Aluno>
{
    public AlunoValidator()
    {
        RuleFor(t => t.Nome)
            .NotEmpty().WithMessage("Nome requerido")
            .MinimumLength(3).WithMessage("Nome deve ter mínimo de 3 caracteres");
        RuleFor(t => t.Email)
            .EmailAddress()
            .When(t => !string.IsNullOrEmpty(t.Email))
            .WithMessage("Email inválido");
    }
}
