using FluentValidation;
using Subasta.Dominio.Dtos;
using Subasta.Recursos;
using System;
using System.Linq;

namespace Subasta.Dominio.Validacoes
{
    public class ValidadorUsuario : AbstractValidator<UsuarioDto>
    {
        public ValidadorUsuario()
        {
            RuleFor(usuario => usuario.Nome)
                .NotEmpty()
                .WithMessage(MensagensErro.NomeObrigatorio);

            RuleFor(usuario => usuario.Login)
                .NotEmpty()
                .WithMessage(MensagensErro.LoginObrigatorio);

            RuleFor(usuario => usuario.Email)
                .NotEmpty()
                .WithMessage(MensagensErro.EmailObrigatorio);

            RuleFor(usuario => usuario.Senha)
                .Must(x => HasEqualOrMoreThanSixCaracters(x) 
                           && ContainUpperLetter(x) 
                           && ContainLowerLetter(x) 
                           && ContainNumber(x))
                    .WithMessage(MensagensErro.SenhaInvalida);

            RuleFor(usuario => usuario.DataNascimento)
                .NotNull()
                    .WithMessage(MensagensErro.DataNascimentoObrigatoria)
                .InclusiveBetween(DateTime.Today.AddYears(-130), DateTime.Today.AddYears(-18))
                    .WithMessage(MensagensErro.ApenasMaioresDeDezoito);
        }

        private static bool HasEqualOrMoreThanSixCaracters(string x)
        {
            return x.Length > 6;
        }

        private static bool ContainNumber(string x)
        {
            return x.Any(char.IsNumber);
        }

        private static bool ContainLowerLetter(string x)
        {
            return x.Any(char.IsLower);
        }

        private static bool ContainUpperLetter(string x)
        {
            return x.Any(char.IsUpper);
        }
    }
}
