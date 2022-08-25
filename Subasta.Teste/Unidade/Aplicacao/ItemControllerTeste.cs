using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Subasta.Api.Controllers;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Dominio.Interfaces.Servicos;
using Subasta.Recursos;
using Subasta.Teste.Builders;
using System;
using Xunit;

namespace Subasta.Teste.Unidade.Aplicacao
{
    public class ItemControllerTeste
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServicoItem _servico;
        private readonly ItemController _controller;

        public ItemControllerTeste()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _servico = Substitute.For<IServicoItem>();
            _controller = new ItemController(_servico, _unitOfWork);
        }

        [Fact]
        public void Adicionar_deve_salvar_item()
        {
            //arrange
            var item = new ItemBuilder().ConstruirDto();

            //act
            _controller.Adicionar(item);

            //assert
            _servico.Received(1).Adicionar(item);
        }

        [Fact]
        public void Adicionar_deve_retornar_bad_request_quando_item_for_invalido()
        {
            //arrange
            var item = new ItemBuilder()
                .ComNome(string.Empty)
                .ConstruirDto();

            _servico.When(s => s.Adicionar(item))
                .Do(x =>
                {
                    throw new Exception(MensagensErro.NomeObrigatorio);
                });

            //act
            var retorno = _controller.Adicionar(item);

            //assert
            retorno.Should().BeOfType<BadRequestObjectResult>();
        }
    
        [Fact]
        public void AdicionarLance_deve_adicionar_novo_lance_para_o_item()
        {
            //arrange
            var lance = new LanceDto()
            {
                UsuarioId = Guid.NewGuid(),
                ItemId = Guid.NewGuid(),
                Valor = 2000
            };

            //act
            _controller.AdicionarLance(lance);

            //assert
            _servico.Received(1).AdicionarLance(lance);
        }

        [Fact]
        public void Encerrar_deve_definir_comprador_do_item()
        {
            //arrange
            var item = new ItemBuilder().Construir();

            //act
            _controller.Encerrar(item.Id);

            //assert
            _servico.Received(1).Encerrar(item.Id);
        }
    }
}
