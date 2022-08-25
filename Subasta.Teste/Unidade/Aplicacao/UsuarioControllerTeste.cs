using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Subasta.Api.Controllers;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Interfaces.Servicos;
using Subasta.Recursos;
using Subasta.Teste.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Subasta.Teste.Unidade.Aplicacao
{
    public class UsuarioControllerTeste
    {
        private readonly UsuarioController _controller;
        private readonly IServicoUsuario _servico;

        public UsuarioControllerTeste()
        {
            _servico = Substitute.For<IServicoUsuario>();
            _controller = new UsuarioController(_servico);
        }

        [Fact]
        public void Adicionar_deve_salvar_usuario()
        {
            //arrange
            var usuarioParaCriar = new UsuarioBuilder().ConstruirDto();

            //act
            var retorno = _controller.Adicionar(usuarioParaCriar);

            //assert
            _servico.Received(1).Adicionar(usuarioParaCriar);
            retorno.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void Adicionar_deve_retornar_bad_request_quando_usuario_for_invalido()
        {
            //arrange
            var usuarioParaCriar = new UsuarioBuilder()
                .ComLogin(string.Empty)
                .ConstruirDto();

            _servico.When(s => s.Adicionar(usuarioParaCriar))
                .Do(x =>
                {
                    throw new Exception(MensagensErro.LoginObrigatorio);
                });

            //act
            var retorno = _controller.Adicionar(usuarioParaCriar);

            //assert
            retorno.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
