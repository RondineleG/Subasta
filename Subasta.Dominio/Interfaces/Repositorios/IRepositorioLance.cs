using Subasta.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioLance : IRepositorio<Lance>
    {
        bool ItemTemLanceComValorMaiorOuIgual(Guid itemId, decimal valor);
        Lance BuscarLanceVencedor(Guid id);
    }
}
