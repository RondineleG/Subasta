using Subasta.Dominio.Dtos;
using System;

namespace Subasta.Dominio.Interfaces.Servicos
{
    public interface IServicoItem
    {
        void Adicionar(ItemDto itemDto);
        void AdicionarLance(LanceDto lanceDto);
        void Encerrar(Guid id);
    }
}
