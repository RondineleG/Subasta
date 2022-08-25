using FluentValidation;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Entidades;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Dominio.Interfaces.Servicos;
using Subasta.Dominio.Validacoes;
using Subasta.Recursos;
using Subasta.Recursos.Extensoes;
using System;
using System.Linq;

namespace Subasta.Dominio.Servicos
{
    public class ServicoUsuario : IServicoUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ServicoUsuario(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public void Adicionar(UsuarioDto usuarioDto)
        {
            ValidarUsuario(usuarioDto);

            var usuarioParaAdicionar = new Usuario(usuarioDto);
            _repositorioUsuario.Adicionar(usuarioParaAdicionar);
        }

        private void ValidarUsuario(UsuarioDto usuarioDto)
        {
            var validador = new ValidadorUsuario();
            var resultadoValidacao = validador.Validate(usuarioDto);

            if (!resultadoValidacao.IsValid)
                throw new Exception(resultadoValidacao.GetErros());

            if (LoginEstaEmUso(usuarioDto.Login))
                throw new Exception(MensagensErro.LoginEstaEmUso);
        }

        private bool LoginEstaEmUso(string login)
        {
            return _repositorioUsuario.LoginEstaEmUso(login);
        }
    }
}
