using Microsoft.EntityFrameworkCore;
using Subasta.Dominio.Entidades;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Infra.Repositorios.Base;
using System;
using System.Linq;

namespace Subasta.Infra.Repositorios
{
    public class RepositorioLeilao : RepositorioBase<Leilao>, IRepositorioLeilao
    {
        public RepositorioLeilao(DbContext context) : base(context)
        {
        }

        public Leilao BuscarPorIdComItens(Guid leilaoId)
        {
            return _set.Include(x => x.Itens).FirstOrDefault(x => x.Id == leilaoId);
        }
    }
}
