using FluentAssertions;
using NSubstitute;
using Subasta.Dominio.Entidades;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Dominio.Interfaces.Servicos;
using Subasta.Dominio.Servicos;
using Subasta.Recursos;
using Subasta.Teste.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Subasta.Teste.Unidade.Dominio.Servicos
{
    public class ServicoUsuarioTeste
    {
        private readonly IServicoUsuario _servico;
        private readonly IRepositorioUsuario _repositorio;

        public ServicoUsuarioTeste()
        {
            _repositorio = Substitute.For<IRepositorioUsuario>();
            _servico = new ServicoUsuario(_repositorio);
        }

        [Fact]
        public void Adicionar_deve_salvar_um_novo_usuario()
        {
            //arrange
            var usuarioDto = new UsuarioBuilder().ConstruirDto();

            //act
            _servico.Adicionar(usuarioDto);

            //assert
            _repositorio.Received(1).Adicionar(Arg.Is<Usuario>(usuario =>
                usuario.Login == usuarioDto.Login
                && usuario.Nome == usuarioDto.Nome
                && usuario.Senha == usuarioDto.Senha
                && usuario.Email == usuarioDto.Email
                && usuario.DataNascimento == usuarioDto.DataNascimento));
        }

        [Fact]
        public void Adicionar_nao_deve_salvar_usuario_invalido()
        {
            //arrange
            var usuario = new UsuarioBuilder()
                .ComSenha("Mn1s")
                .ConstruirDto();

            //act
            Action action = () => _servico.Adicionar(usuario);

            //assert
            action
                .Should()
                .Throw<Exception>()
                .WithMessage(MensagensErro.SenhaInvalida);

            _repositorio.DidNotReceive().Adicionar(Arg.Any<Usuario>());
        }

        [Fact]
        public void Adicionar_nao_deve_salvar_um_novo_usuario_quando_o_login_ja_estiver_em_uso()
        {
            //arrange
            var usuarioDto = new UsuarioBuilder().ConstruirDto();

            _repositorio.LoginEstaEmUso(usuarioDto.Login).Returns(true);
            
            //act
            Action action = () => _servico.Adicionar(usuarioDto);

            //assert
            action
                .Should()
                .Throw<Exception>()
                .WithMessage(MensagensErro.LoginEstaEmUso);

            _repositorio.DidNotReceive().Adicionar(Arg.Any<Usuario>());
        }
    }
}
