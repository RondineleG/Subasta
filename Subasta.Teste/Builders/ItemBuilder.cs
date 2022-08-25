using Subasta.Dominio.Dtos;
using Subasta.Dominio.Entidades;
using System;

namespace Subasta.Teste.Builders
{
    public class ItemBuilder
    {
        private string _nome = "Carro usado novo em folha";
        private string _descricao = "Pensando mais a longo prazo, o entendimento das metas propostas exige a precisão e a definição das condições financeiras e administrativas exigidas.";
        private decimal _valorInicial = 23050;
        private Guid _leilaoId;

        public Item Construir()
        {
            return new Item(ConstruirDto());
        }

        public ItemDto ConstruirDto()
        {
            return new ItemDto()
            {
                Nome = _nome,
                Descricao = _descricao,
                LeilaoId = _leilaoId,
                ValorInicial = _valorInicial
            };
        }

        public ItemBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public ItemBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public ItemBuilder ComValorInicial(decimal valorInicial)
        {
            _valorInicial = valorInicial;
            return this;
        }

        public ItemBuilder ComLeilao(Guid leilaoId)
        {
            _leilaoId = leilaoId;
            return this;
        }
    }
}
