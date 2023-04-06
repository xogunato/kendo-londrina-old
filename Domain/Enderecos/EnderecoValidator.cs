using FluentValidation;

namespace w_escolas.Domain.Enderecos;

public class EnderecoValidator : AbstractValidator<Endereco>
{
    public EnderecoValidator()
    {
        RuleFor(t => t.Uf)
          .NotEmpty();
        RuleFor(t => t.Cidade)
            .NotEmpty()
            .MinimumLength(3);
    }
}
