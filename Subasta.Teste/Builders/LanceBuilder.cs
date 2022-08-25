using Subasta.Dominio.Dtos;
using Subasta.Dominio.Entidades;
using System;

namespace Subasta.Teste.Builders
{
    public class LanceBuilder
    {
        private Guid _itemId = Guid.NewGuid();
        private readonly Guid _userId = Guid.NewGuid();
        private readonly decimal _valor = 30912;

        public Lance Construir()
        {
            return new Lance(ConstruirDto());
        }

        public LanceDto ConstruirDto()
        {
            return new LanceDto()
            {
                ItemId = _itemId,
                UsuarioId = _userId,
                Valor = _valor
            };
        }

        public LanceBuilder ComItem(Guid itemId)
        {
            _itemId = itemId;
            return this;
        }
    }
}
