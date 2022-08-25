using Subasta.Dominio.Dtos;
using System;

namespace Subasta.Dominio.Entidades
{
    public class Lance : EntidadeBase
    {
        public Item Item { get; private set; }
        public Guid ItemId { get; private set; }
        public Usuario Usuario { get; private set; }
        public Guid UsuarioId { get; private set; }
        public decimal Valor { get; private set; }

        protected Lance()
        {
        }

        public Lance(LanceDto lanceDto)
        {
            ItemId = lanceDto.ItemId;
            UsuarioId = lanceDto.UsuarioId;
            Valor = lanceDto.Valor;
        }
    }
}
