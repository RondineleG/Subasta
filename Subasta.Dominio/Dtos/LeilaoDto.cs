using FluentValidation.Results;
using Subasta.Dominio.Validacoes;
using System;
using System.Collections.Generic;

namespace Subasta.Dominio.Dtos
{
    public class LeilaoDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public IList<ItemDto> Itens { get; set; }

        
    }
}
