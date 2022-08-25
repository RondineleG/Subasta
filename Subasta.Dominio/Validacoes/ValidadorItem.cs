using FluentValidation;
using Subasta.Dominio.Dtos;
using Subasta.Recursos;

namespace Subasta.Dominio.Validacoes
{
    public class ValidadorItem : AbstractValidator<ItemDto>
    {
        public ValidadorItem()
        {
            RuleFor(item => item.Nome)
                .NotEmpty()
                .WithMessage(MensagensErro.NomeObrigatorio);

            RuleFor(item => item.Descricao)
                .NotEmpty()
                .WithMessage(MensagensErro.DescricaoObrigatoria);

            RuleFor(item => item.ValorInicial)
                .GreaterThan(0)
                .WithMessage(MensagensErro.ValorInicialMaiorQueZero);
        }
    }
}
