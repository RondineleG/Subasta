using System;

namespace Subasta.Dominio.Dtos
{
    public class LanceDto
    {
        public Guid ItemId { get; set; }
        public Guid UsuarioId { get; set; }
        public decimal Valor { get; set; }
    }
}
