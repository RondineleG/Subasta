using Subasta.Dominio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Subasta.Dominio.Entidades
{
    public class Leilao : EntidadeBase
    {
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFinal { get; private set; }
        public IList<Item> Itens { get; private set; }

        protected Leilao()
        {
        }

        public Leilao(LeilaoDto leilao)
        {
            DataInicio = leilao.DataInicio;
            Itens = leilao.Itens.Select(i => new Item(i)).ToList();
        }

        public void Encerrar()
        {
            DataFinal = DateTime.Now;
        }
    }
}
