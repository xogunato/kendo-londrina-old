using FluentValidation;
using Microsoft.VisualBasic;

namespace w_escolas.Domain.Temporadas;

public class TemporadaValidator : AbstractValidator<Temporada>
{
    private bool validYear(string? ano)
    {
        if (ano is null)
            return true;
        return false;
    }
    public TemporadaValidator()
    {
        RuleFor(t => t.Codigo)
            .NotEmpty();
        RuleFor(t => t.Nome)
            .NotEmpty()
            .MinimumLength(3);
        When(t => t.Ano is not null, () =>
        {
            RuleFor(t => t.Ano)
            .GreaterThanOrEqualTo(1980)
                .WithMessage("Informar Ano entre 1980 e 2080")
            .LessThanOrEqualTo(2080)
                .WithMessage("Informar Ano entre 1980 e 2080");
            When(t => t.Semestre is not null, () =>
            {
                RuleFor(t => t.Semestre)
                    .GreaterThanOrEqualTo(1)
                    .LessThanOrEqualTo(2);
            });
            When(t => t.Quadrimestre is not null, () =>
            {
                RuleFor(t => t.Quadrimestre)
                    .GreaterThanOrEqualTo(1)
                    .LessThanOrEqualTo(3);
            });
            When(t => t.Trimestre is not null, () =>
            {
                RuleFor(t => t.Trimestre)
                    .GreaterThanOrEqualTo(1)
                    .LessThanOrEqualTo(4);
            });
            When(t => t.Bimestre is not null, () =>
            {
                RuleFor(t => t.Bimestre)
                    .GreaterThanOrEqualTo(1)
                    .LessThanOrEqualTo(6);
            });
            When(t => t.Mes is not null, () =>
            {
                RuleFor(t => t.Mes)
                    .GreaterThanOrEqualTo(1)
                    .LessThanOrEqualTo(12);
            });
        });
        When(t => t.Ano is null, () =>
        {
            RuleFor(t => t.Semestre)
                .Null().When(t => t.Ano is null)
                    .WithMessage("Informar Ano para validar o Semestre");
            RuleFor(t => t.Quadrimestre)
                .Null().When(t => t.Ano is null)
                    .WithMessage("Informar Ano para validar o Quadrimestre");
            RuleFor(t => t.Trimestre)
                .Null().When(t => t.Ano is null)
                    .WithMessage("Informar Ano para validar o Trimestre");
            RuleFor(t => t.Bimestre)
                .Null().When(t => t.Ano is null)
                    .WithMessage("Informar Ano para validar o Bimestre");
            RuleFor(t => t.Mes)
                .Null().When(t => t.Ano is null)
                    .WithMessage("Informar Ano para validar o Mes");
        });
    }
}
