using Microsoft.EntityFrameworkCore;
using Subasta.Dominio.Entidades;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Infra.Repositorios.Base;
using System;
using System.Linq;

namespace Subasta.Infra.Repositorios
{
    public class RepositorioLance : RepositorioBase<Lance>, IRepositorioLance
    {
        public RepositorioLance(DbContext context) : base(context)
        {
        }

        public Lance BuscarLanceVencedor(Guid itemId)
        {
            return _set.Where(x => x.ItemId == itemId).Aggregate((x, y) => x.Valor > y.Valor ? x : y);
        }

        public bool ItemTemLanceComValorMaiorOuIgual(Guid itemId, decimal valor)
        {
            return _set.Any(x => x.Valor >= valor);
        }
    }
}
