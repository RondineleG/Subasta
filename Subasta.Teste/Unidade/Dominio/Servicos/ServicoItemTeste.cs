using FluentAssertions;
using NSubstitute;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Entidades;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Dominio.Interfaces.Servicos;
using Subasta.Dominio.Servicos;
using Subasta.Recursos;
using Subasta.Teste.Builders;
using System;
using Xunit;

namespace Subasta.Teste.Unidade.Dominio.Servicos
{
    public class ServicoItemTeste
    {
        private readonly IRepositorioLeilao _repositorioLeilao;
        private readonly IRepositorioLance _repositorioLance;
        private readonly IRepositorioItem _repositorioItem;
        private readonly IServicoItem _servico;

        public ServicoItemTeste()
        {
            _repositorioLeilao = Substitute.For<IRepositorioLeilao>();
            _repositorioLance = Substitute.For<IRepositorioLance>();
            _repositorioItem = Substitute.For<IRepositorioItem>();

            _servico = new ServicoItem(_repositorioLeilao, _repositorioLance, _repositorioItem);
        }

        [Fact]
        public void Adicionar_deve_salvar_item()
        {
            //arrange
            var leilao = new LeilaoBuilder().Construir();
            var item = new ItemBuilder()
                .ComNome("Casa nova")
                .ComValorInicial(430900)
                .ConstruirDto();

            _repositorioLeilao.BuscarPorIdComItens(item.LeilaoId).Returns(leilao);

            //act
            _servico.Adicionar(item);

            //assert
            leilao.Itens.Should().Contain(x => x.Nome == item.Nome && x.ValorInicial == item.ValorInicial);
        }

        [Fact]
        public void Adicionar_nao_deve_salvar_item_quando_ele_esta_invalido()
        {
            //arrange
            var leilao = new LeilaoBuilder().Construir();
            var item = new ItemBuilder()
                .ComNome(string.Empty)
                .ConstruirDto();

            _repositorioLeilao.BuscarPorId(item.LeilaoId).Returns(leilao);

            //act
            Action action = () => _servico.Adicionar(item);

            //assert
            action
                .Should()
                .Throw<Exception>()
                .WithMessage(MensagensErro.NomeObrigatorio);

            leilao.Itens.Should().NotContain(x => string.IsNullOrEmpty(x.Nome));
        }
    
        [Fact]
        public void AdicionarLance_deve_salvar_novo_lance_para_item()
        {
            //arrange
            var item = new ItemBuilder().Construir();
            var lance = new LanceDto()
            {
                ItemId = item.Id,
                UsuarioId = Guid.NewGuid(),
                Valor = 50000
            };

            _repositorioItem.BuscarPorId(item.Id).Returns(item);
            _repositorioLance.ItemTemLanceComValorMaiorOuIgual(item.Id, lance.Valor).Returns(false);

            //act
            _servico.AdicionarLance(lance);

            //assert
            _repositorioLance.Received(1).Adicionar(Arg.Is<Lance>(x =>
                x.ItemId == lance.ItemId
                && x.UsuarioId == lance.UsuarioId
                && x.Valor == lance.Valor));
        }

        [Fact]
        public void AdicionarLance_nao_deve_salvar_novo_lance_para_item_se_valor_for_menor_que_o_minimo()
        {
            //arrange
            var item = new ItemBuilder().Construir();
            var lance = new LanceDto()
            {
                ItemId = item.Id,
                UsuarioId = Guid.NewGuid(),
                Valor = 2000
            };

            _repositorioItem.BuscarPorId(item.Id).Returns(item);

            //act
            Action adicionarLance = () => _servico.AdicionarLance(lance);

            //assert
            adicionarLance
                .Should()
                .Throw<Exception>()
                .WithMessage(MensagensErro.LanceAbaixoDoValorInicial);

            _repositorioLance.DidNotReceive().Adicionar(Arg.Any<Lance>());
        }

        [Fact]
        public void AdicionarLance_nao_deve_salvar_novo_lance_para_item_se_valor_for_menor_que_o_lance_mais_alto()
        {
            //arrange
            var item = new ItemBuilder().Construir();
            var lance = new LanceDto()
            {
                ItemId = item.Id,
                UsuarioId = Guid.NewGuid(),
                Valor = 50000
            };

            _repositorioItem.BuscarPorId(item.Id).Returns(item);
            _repositorioLance.ItemTemLanceComValorMaiorOuIgual(item.Id, lance.Valor).Returns(true);

            //act
            Action adicionarLance = () => _servico.AdicionarLance(lance);

            //assert
            adicionarLance
                .Should()
                .Throw<Exception>()
                .WithMessage(MensagensErro.ValorLanceAbaixoDoUltimo);

            _repositorioLance.DidNotReceive().Adicionar(Arg.Any<Lance>());
        }

        [Fact]
        public void AdicionarLance_nao_deve_salvar_novo_lance_para_item_que_nao_existe()
        {
            //arrange
            var lance = new LanceDto()
            {
                ItemId = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid(),
                Valor = 50000
            };

            //act
            Action adicionarLance = () => _servico.AdicionarLance(lance);

            //assert
            adicionarLance
                .Should()
                .Throw<Exception>()
                .WithMessage(MensagensErro.ItemNaoEncontrado);

            _repositorioLance.DidNotReceive().Adicionar(Arg.Any<Lance>());
        }

        [Fact]
        public void Encerrar_deve_definir_comprador_do_item()
        {
            //arrange
            var item = new ItemBuilder()
                .Construir();

            var lance = new LanceBuilder()
                .ComItem(item.Id)
                .Construir();

            _repositorioItem.BuscarPorId(item.Id).Returns(item);
            _repositorioLance.BuscarLanceVencedor(item.Id).Returns(lance);

            //act
            _servico.Encerrar(item.Id);

            //assert
            item.CompradorId.Should().Be(lance.UsuarioId);
            item.ValorFinal.Should().Be(lance.Valor);
        }
    }
}
