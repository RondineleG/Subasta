using Subasta.Dominio.Entidades;
using System;

namespace Subasta.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioLeilao : IRepositorio<Leilao>
    {
        Leilao BuscarPorIdComItens(Guid leilaoId);
    }
}
