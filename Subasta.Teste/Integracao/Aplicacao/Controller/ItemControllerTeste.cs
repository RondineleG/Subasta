using Subasta.Api;
using Subasta.Teste.Builders;
using Subasta.Teste.Integracao.Infra;
using System.Net.Http;
using System.Threading.Tasks;
using Subasta.Recursos.Extensoes;
using Xunit;
using System;
using Subasta.Dominio.Entidades;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Subasta.Recursos;

namespace Subasta.Teste.Integracao.Aplicacao.Controller
{
    public class ItemControllerTeste
    {
        private SubastaWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public ItemControllerTeste()
        {
            _factory = new SubastaWebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Adicionar_deve_salvar_um_novo_item_no_leilao()
        {
            //arrange
            var leilao = new LeilaoBuilder().Construir();
            _factory.AdicionarEntidades(leilao);

            var itemParaAdicionar = new ItemBuilder()
                .ComNome("Banana premiada")
                .ComDescricao("Vale muitos dinheiros")
                .ComValorInicial(2400)
                .ComLeilao(leilao.Id)
                .ConstruirDto();

            var builder = new UriBuilder($"{_client.BaseAddress}api/item");

            //act
            var response = await _client.PostAsJsonAsync(builder.Uri, itemParaAdicionar);
            response.EnsureSuccessStatusCode();

            //assert
            using var contexto = _factory.GetContexto();
            var leilaoComItem = contexto
                .Set<Leilao>()
                .Include(x => x.Itens)
                .FirstOrDefault(x => x.Id == leilao.Id);

            leilaoComItem.Itens.Should().HaveCount(2);
            leilaoComItem.Itens.Should().Contain(x => 
                x.Nome == itemParaAdicionar.Nome
                && x.Descricao == itemParaAdicionar.Descricao
                && x.ValorInicial == itemParaAdicionar.ValorInicial);
        }

        [Fact]
        public async Task Adicionar_nao_deve_salvar_um_novo_item_no_leilao_caso_este_seja_invalido_e_retorne_bad_request()
        {
            //arrange
            var leilao = new LeilaoBuilder().Construir();
            _factory.AdicionarEntidades(leilao);

            var itemParaAdicionar = new ItemBuilder()
                .ComNome("Banana premiada")
                .ComDescricao(string.Empty)
                .ComValorInicial(0)
                .ComLeilao(leilao.Id)
                .ConstruirDto();

            var builder = new UriBuilder($"{_client.BaseAddress}api/item");

            //act
            var resposta = await _client.PostAsJsonAsync(builder.Uri, itemParaAdicionar);

            //assert
            var corpoResposta = await resposta.Content.ReadAsStringAsync();
            corpoResposta.Should().Contain(MensagensErro.DescricaoObrigatoria);
            corpoResposta.Should().Contain(MensagensErro.ValorInicialMaiorQueZero);
        }
    }
}