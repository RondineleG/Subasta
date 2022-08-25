using FluentAssertions;
using NSubstitute;
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
    public class ServicoLeilaoTeste
    {
        private readonly IRepositorioLeilao _repositorio;
        private readonly IServicoLeilao _servico;

        public ServicoLeilaoTeste()
        {
            _repositorio = Substitute.For<IRepositorioLeilao>();
            _servico = new ServicoLeilao(_repositorio);
        }

        [Fact]
        public void Adicionar_deve_salvar_leilao()
        {
            //arrange
            var leilao = new LeilaoBuilder().ConstruirDto();

            //act
            _servico.Adicionar(leilao);

            //assert
            _repositorio.Received(1).Adicionar(Arg.Is<Leilao>(l =>
                l.Itens.Count == leilao.Itens.Count
                && l.DataInicio == leilao.DataInicio));
        }

        [Fact]
        public void Adicionar_nao_deve_salvar_leilao_invalido()
        {
            //arrange
            var leilao = new LeilaoBuilder()
                .ComDataInicio(DateTime.Now.AddMinutes(-20))
                .ConstruirDto();

            //act
            Action action = () => _servico.Adicionar(leilao);

            //assert
            action
                .Should()
                .Throw<Exception>()
                .WithMessage(MensagensErro.DataInicioObrigatoria);

            _repositorio.DidNotReceive().Adicionar(Arg.Any<Leilao>());
        }
    
        [Fact]
        public void Encerrar_deve_atualizar_data_fim_e_encerrar_leilao()
        {
            //arrange
            var leilao = new LeilaoBuilder().Construir();
            var dataInicio = DateTime.Now;

            _repositorio.BuscarPorId(leilao.Id).Returns(leilao);

            //act
            _servico.Encerrar(leilao.Id);

            //assert
            leilao.DataFinal.Should().BeAfter(dataInicio);
        }

        [Fact]
        public void Encerrar_deve_lancar_excecao_quando_leilao_nao_for_encontrado()
        {
            //arrange
            var leilaoId = Guid.NewGuid();

            //act
            Action encerramentoLeilao = () => _servico.Encerrar(leilaoId);

            //assert
            encerramentoLeilao
                .Should()
                .Throw<Exception>()
                .WithMessage(MensagensErro.LeilaoNaoEncontrado);
        }
    }
}
