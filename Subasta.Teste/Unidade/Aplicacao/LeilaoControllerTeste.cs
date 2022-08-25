using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Subasta.Api.Controllers;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Dominio.Interfaces.Servicos;
using Subasta.Recursos;
using Subasta.Teste.Builders;
using System;
using Xunit;

namespace Subasta.Teste.Unidade.Aplicacao
{
    public class LeilaoControllerTeste
    {
        private readonly IServicoLeilao _servico;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LeilaoController _controller;

        public LeilaoControllerTeste()
        {
            _servico = Substitute.For<IServicoLeilao>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _controller = new LeilaoController(_servico, _unitOfWork);
        }

        [Fact]
        public void Adicionar_deve_salvar_usuario()
        {
            //arrange
            var leilao = new LeilaoBuilder().ConstruirDto();

            //act
            _controller.Adicionar(leilao);

            //assert
            _servico.Received(1).Adicionar(leilao);
        }

        [Fact]
        public void Adicionar_deve_retornar_bad_request_quando_leilao_for_invalido()
        {
            //arrange
            var leilao = new LeilaoBuilder()
                .ComDataInicio(DateTime.Today.AddDays(-2))
                .ConstruirDto();

            _servico.When(s => s.Adicionar(leilao))
                .Do(x =>
                {
                    throw new Exception(MensagensErro.DataInicioObrigatoria);
                });

            //act
            var retorno = _controller.Adicionar(leilao);

            //assert
            retorno.Should().BeOfType<BadRequestObjectResult>();
        }
    
        [Fact]
        public void Encerrar_deve_encerrar_o_leilao()
        {
            //arrange
            var leilaoId = Guid.NewGuid();

            //act
            _controller.Encerrar(leilaoId);

            //assert
            _servico.Received(1).Encerrar(leilaoId);
        }
    }
}
