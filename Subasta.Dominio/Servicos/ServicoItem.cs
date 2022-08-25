using FluentValidation;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Entidades;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Dominio.Interfaces.Servicos;
using Subasta.Dominio.Validacoes;
using Subasta.Recursos;
using Subasta.Recursos.Extensoes;
using System;

namespace Subasta.Dominio.Servicos
{
    public class ServicoItem : IServicoItem
    {
        private readonly IRepositorioLeilao _repositorioLeilao;
        private readonly IRepositorioLance _repositorioLance;
        private readonly IRepositorioItem _repositorioItem;

        public ServicoItem(IRepositorioLeilao repositorioLeilao, IRepositorioLance repositorioLance,
            IRepositorioItem repositorioItem)
        {
            _repositorioLeilao = repositorioLeilao;
            _repositorioLance = repositorioLance;
            _repositorioItem = repositorioItem;
        }

        public void Adicionar(ItemDto itemDto)
        {
            ValidarItem(itemDto);

            var leilao = _repositorioLeilao.BuscarPorIdComItens(itemDto.LeilaoId);
            if (leilao == null)
                throw new Exception(MensagensErro.LeilaoNaoEncontrado);

            var item = new Item(itemDto);
            leilao.Itens.Add(item);
        }

        private void ValidarItem(ItemDto itemDto)
        {
            var validador = new ValidadorItem();
            var resultadoValidacao = validador.Validate(itemDto);

            if (!resultadoValidacao.IsValid)
                throw new Exception(resultadoValidacao.GetErros());
        }

        public void AdicionarLance(LanceDto lanceDto)
        {
            var item = _repositorioItem.BuscarPorId(lanceDto.ItemId);

            if (item == null)
                throw new Exception(MensagensErro.ItemNaoEncontrado);

            ValidarLance(lanceDto, item);

            var lance = new Lance(lanceDto);
            _repositorioLance.Adicionar(lance);
        }

        public void Encerrar(Guid id)
        {
            var item = _repositorioItem.BuscarPorId(id);

            if (item == null)
                return;

            var lanceGanhador = _repositorioLance.BuscarLanceVencedor(id);

            if (lanceGanhador == null)
                return;

            item.Encerrar(lanceGanhador);
        }

        private void ValidarLance(LanceDto lanceDto, Item item)
        {
            if (item.ValorInicial > lanceDto.Valor)
                throw new Exception(MensagensErro.LanceAbaixoDoValorInicial);

            if (_repositorioLance.ItemTemLanceComValorMaiorOuIgual(lanceDto.ItemId, lanceDto.Valor))
                throw new Exception(MensagensErro.ValorLanceAbaixoDoUltimo);
        }
    }
}
