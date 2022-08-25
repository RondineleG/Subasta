using Subasta.Dominio.Dtos;
using System;
using System.Collections.Generic;

namespace Subasta.Dominio.Entidades
{
    public class Item : EntidadeBase
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal ValorInicial { get; private set; }
        public decimal ValorFinal { get; private set; }
        public Leilao Leilao { get; private set; }
        public Guid LeilaoId { get; private set; }
        public Usuario Comprador { get; private set; }
        public Guid? CompradorId { get; private set; }
        public IList<Lance> Lances { get; private set; }

        protected Item()
        {
        }

        public Item(ItemDto itemDto)
        {
            Nome = itemDto.Nome;
            Descricao = itemDto.Descricao;
            LeilaoId = itemDto.LeilaoId;
            ValorInicial = itemDto.ValorInicial;
        }

        public void Encerrar(Lance lanceGanhador)
        {
            ValorFinal = lanceGanhador.Valor;
            CompradorId = lanceGanhador.UsuarioId;
        }
    }
}
