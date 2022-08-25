using Subasta.Dominio.Dtos;
using Subasta.Dominio.Entidades;
using System;

namespace Subasta.Teste.Builders
{
    public class UsuarioBuilder
    {
        private string _nome = "João da Silva";
        private string _login = "joao";
        private string _senha = "Silvinha01";
        private string _email = "joao@joao.com";
        private DateTime _dataNascimento = DateTime.Now.AddYears(-20);

        public Usuario Construir()
        {
            return new Usuario(ConstruirDto());
        }

        public UsuarioDto ConstruirDto()
        {
            return new UsuarioDto()
            {
                Nome = _nome,
                Login = _login,
                Senha = _senha,
                Email = _email,
                DataNascimento = _dataNascimento
            };
        }

        public UsuarioBuilder ComLogin(string login)
        {
            _login = login;
            return this;
        }

        public UsuarioBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public UsuarioBuilder ComSenha(string senha)
        {
            _senha = senha;
            return this;
        }

        public UsuarioBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public UsuarioBuilder ComDataNascimento(DateTime dataNascimento)
        {
            _dataNascimento = dataNascimento;
            return this;
        }
    }
}
