using Microsoft.EntityFrameworkCore;
using Subasta.Dominio.Entidades;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Infra.Repositorios.Base;

namespace Subasta.Infra.Repositorios
{
    public class RepositorioItem : RepositorioBase<Item>, IRepositorioItem
    {
        public RepositorioItem(DbContext context) : base(context)
        {
        }
    }
}
