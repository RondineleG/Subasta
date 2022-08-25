using Subasta.Dominio.Dtos;
using Subasta.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace Subasta.Teste.Builders
{
    public class LeilaoBuilder
    {
        private DateTime _dataInicio = DateTime.Now.AddDays(5);
        private IList<ItemDto> _itens = new List<ItemDto>() { new ItemBuilder().ConstruirDto() };

        public Leilao Construir()
        {
            return new Leilao(ConstruirDto());
        }

        public LeilaoDto ConstruirDto()
        {
            return new LeilaoDto()
            {
                DataInicio = _dataInicio,
                Itens = _itens
            };
        }

        public LeilaoBuilder ComDataInicio(DateTime dataInicio)
        {
            _dataInicio = dataInicio;
            return this;
        }

        public LeilaoBuilder ComItens(IList<ItemDto> itens)
        {
            _itens = itens;
            return this;
        }
    }
}
