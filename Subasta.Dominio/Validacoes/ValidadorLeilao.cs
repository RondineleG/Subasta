using FluentValidation;
using Subasta.Dominio.Dtos;
using Subasta.Recursos;
using System;

namespace Subasta.Dominio.Validacoes
{
    public class ValidadorLeilao : AbstractValidator<LeilaoDto>
    {
        public ValidadorLeilao()
        {
            RuleFor(leilao => leilao.DataInicio)
                .GreaterThan(DateTime.Now)
                .WithMessage(MensagensErro.DataInicioObrigatoria);

            RuleFor(leilao => leilao.Itens)
                .NotEmpty()
                .WithMessage(MensagensErro.ItemObrigatorio);

            RuleForEach(leilao => leilao.Itens)
                .SetValidator(new ValidadorItem());
        }
    }
}
