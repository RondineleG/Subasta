using Subasta.Dominio.Dtos;

namespace Subasta.Dominio.Interfaces.Servicos
{
    public interface IServicoLeilao
    {
        void Adicionar(LeilaoDto leilaoDto);
        void Encerrar(System.Guid leilaoId);
    }
}
