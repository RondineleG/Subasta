using FluentValidation.Results;
using Subasta.Dominio.Validacoes;
using System;

namespace Subasta.Dominio.Dtos
{
    public class UsuarioDto
    {
        public string Nome { get;  set; }
        public string Login { get;  set; }
        public string Senha { get;  set; }
        public string Email { get;  set; }
        public DateTime DataNascimento { get;  set; }
    }
}
