using FluentValidation.Results;
using System.Linq;

namespace Subasta.Recursos.Extensoes
{
    public static class ValidationResultExtensoes
    {
        public static string GetErros(this ValidationResult resultado)
        {
            var erros = resultado.Errors.Select(x => x.ErrorMessage).ToList();
            return string.Join(" ", erros);
        }
    }
}
