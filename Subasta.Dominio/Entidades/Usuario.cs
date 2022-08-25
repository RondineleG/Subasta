using Subasta.Dominio.Dtos;
using System;

namespace Subasta.Dominio.Entidades
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Email { get; private set; }
        public DateTime DataNascimento { get; private set; }

        protected Usuario()
        {
        }

        public Usuario(UsuarioDto dto)
        {
            Nome = dto.Nome;
            Login = dto.Login;
            Senha = dto.Senha;
            Email = dto.Email;
            DataNascimento = dto.DataNascimento;
        }
    }
}
