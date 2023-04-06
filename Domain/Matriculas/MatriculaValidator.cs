using FluentValidation;

namespace w_escolas.Domain.Matriculas;

public class MatriculaValidator : AbstractValidator<Matricula>
{
    public MatriculaValidator()
    {
        RuleFor(t => t.DataMatricula)
            .NotEmpty();
        RuleFor(t => t.Cancelada)
            .Must(x => x == false || x == true);
        //    .NotEmpty();
    }
}
