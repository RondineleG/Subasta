using System;

namespace Subasta.Dominio.Dtos
{
    public class ItemDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorInicial { get; set; }
        public Guid LeilaoId { get; set; }
    }
}
